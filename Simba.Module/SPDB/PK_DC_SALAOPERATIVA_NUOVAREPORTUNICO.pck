CREATE OR REPLACE PACKAGE PK_DC_SALAOPERATIVA is

  --- dichiarazione dei cursori statici
  TYPE T_CURSOR IS REF CURSOR;
  --cursor cSkAtt (pScenario number) is

procedure RISORSE_EDIFICIO_RDL(iOidEdificio            in number,
                                 iIsSmartphone           in number,
                                 iOidCentroOperativoBase in number,
                                 iusername               in varchar2,
                                 oMessaggio              out varchar2,
                                 IO_CURSOR               IN OUT T_CURSOR);
procedure REPORT_RDL
(
icodiciRDL   in varchar2,
oMessaggio  out varchar2,
IO_CURSOR IN OUT T_CURSOR
); 

end PK_DC_SALAOPERATIVA;
/
create or replace package body PK_DC_SALAOPERATIVA is
  -- Schedulazione Manutenzione programmata Settimanale

  procedure RISORSE_EDIFICIO_RDL(iOidEdificio            in number,
                                 iIsSmartphone           in number,
                                 iOidCentroOperativoBase in number,
                                 iusername               in varchar2,
                                 oMessaggio              out varchar2,
                                 IO_CURSOR               IN OUT T_CURSOR) is
  
    V_CURSOR     T_CURSOR;
    OidEdificio  Edificio.oid%type;
    IsSmartphone number;
  begin
  
    OidEdificio  := iOidEdificio;
    IsSmartphone := iIsSmartphone;
    if iIsSmartphone != 2 then
      IsSmartphone := 0;
    end if;

  
    --- if iIsSmartphone = 2 then  
    OPEN V_CURSOR FOR
      select co.descrizione as CentroOperativo,
             co.oid as OidCentroOperativo,
             ri.oid as OidRisorsaTeam,
             Nvl(regrdlultimo.statooperativo, 'non definito') as ultimostatooperativo,
             t.nome || ' ' || t.cognome as RisorsaCapoSquadra,
             m.descrizione as Mansione,
             case
               when regrdlag.nrattagenda is null then
                0
               else
                regrdlag.nrattagenda
             end as nrattagenda,
             case
               when regrdlsos.nrattsospese is null then
                0
               else
                regrdlsos.nrattsospese
             end as nrattsospese,
             
             case
               when regrdlemerg.nrattemergenza is null then
                0
               else
                regrdlemerg.nrattemergenza
             end as nrattemergenza,
             -- regrdlemerg.nrattemergenza as nrattemergenza,
             Nvl(t.telefono, 'ND') as telefono,
             Nvl(regrdlultimo.descrizione, 'non in Servizio') as regrdlassociato, --descrizione ultimo registro associato
             
            ar.nr_risorse as numman, --numrisorse ssociate al team
          --   Nvl(u."UserName", 'no Dispositivo') as username, --nomerisolto
             distedificio.DISTANZA as distanzaimpianto,
           --  distedificio.EDIFICIO as edificioregrdl,
             Nvl(regrdlultimo.cod_edificio, 'Non Definito') as ultimoedificio,
             ----------  AG ---------------------------
             (select case
                       when count(rdl.oid) = 0 then
                        'nessun intervento'
                       else
                        'nr. ' || count(rdl.oid)
                     -- regrdlemerg.nrattemergenza
                     end as interventisuedificio
              -- 'nr interventi per edificio ' || count(rdl.oid)
                from rdl
               where rdl.edificio = OidEdificio -------------------------------########################
                 and rdl.risorsateam = ri.oid
                 and rdl.datarichiesta > (sysdate - 90)) as interventisuedificio
             
             ----------------  ag  -------------------
             ,
             distedificio.edificio as oidedificio,
             distedificio.url,
             Nvl(t.azienda, 'Non Definito') as azienda, --  Conduttore = 2, centro operativo base  = 1; resto = zero 
             case
               when conduttore.countconduttore is null then
                (case
                  when co.oid = iOidCentroOperativoBase then
                   1
                  else
                   0
                end)
               else
                2
             end as ORDINAMENTO -- iOidCentroOperativoBase
             
             ,
             Nvl(conduttore.countconduttore, 0) as conduttore
      
        from risorseteam ri,
             RISORSE t,
          --   (select u."Oid", u."UserName" from "SecuritySystemUser" u) u,
             centrooperativo co,
             mansioni m --, statooperativo so
             ,
             (select regrdl.risorsateam, count(regrdl.rowid) as nrattagenda
                from regrdl, RDL 
               where regrdl.ultimostatosmistamento in (2)
                 and regrdl.ultimostatooperativo = 19
                 and regrdl.risorsateam is not null
                 ----------
                 AND RDL.REGRDL = REGRDL.OID
                 AND RDL.EDIFICIO = OidEdificio
                 --------
               group by regrdl.risorsateam) regrdlag,
             
             (select regrdl.risorsateam, count(regrdl.rowid) as nrattsospese
                from regrdl, RDL 
               where regrdl.ultimostatosmistamento in (3)
                 and regrdl.ultimostatooperativo in (6, 7, 8, 9, 10)
                 and regrdl.risorsateam is not null
                       ----------
                 AND RDL.REGRDL = REGRDL.OID
                 AND RDL.EDIFICIO = OidEdificio
                 --------
               group by regrdl.risorsateam) regrdlsos,
             
             (select regrdl.risorsateam, count(regrdl.rowid) as nrattemergenza
                from regrdl, RDL 
               where regrdl.ultimostatosmistamento in (10)
                 and regrdl.risorsateam is not null
                       ----------
                 AND RDL.REGRDL = REGRDL.OID
                 AND RDL.EDIFICIO = OidEdificio
                 --------
               group by regrdl.risorsateam) regrdlemerg,
             
             (select cr.risorsateam, count(cr.oid) as countconduttore
                from conduttori cr
               where cr.edificio = OidEdificio
              
               group by cr.risorsateam) Conduttore, --  conduttore 
             
             (select k.risorsateam,
                     regrdl.oid,
                     regrdl.descrizione,
                     e.oid,
                     e.cod_descrizione as cod_edificio,
                     so.statooperativo
                from regrdl,
                     edificio e,
                     impianto i,
                     apparato a,
                     statooperativo so,
                     (select v.risorsateam,
                             v.maxdata,
                             max(regrdl.oid) as maxregrdl
                      --so.statooperativo,
                      --regrdl.oid, rdl.edificio,rdl.impianto
                        from regrdl,RDL,
                             regoperativodettaglio rod --,rdl
                             --,statooperativo so
                            ,
                             (select regrdl.risorsateam,
                                     max(t.dataora) as maxdata
                                from regoperativodettaglio t, regrdl, RDL
                               where regrdl.oid = t.regrdl
                               ----------
                               AND RDL.REGRDL = REGRDL.OID
                               AND RDL.EDIFICIO = OidEdificio   ----------------####@@@@@@@@@
                               ------
                               group by regrdl.risorsateam) v
                       where v.maxdata = rod.dataora
                         and rod.regrdl = regrdl.oid
                         ---------------
                         AND RDL.REGRDL = regrdl.oid
                         AND RDL.Edificio = OidEdificio   ----------------####@@@@@@@@@
                       ------------------------
                       group by v.risorsateam, v.maxdata) k
               where k.maxregrdl = regrdl.oid
                 and regrdl.apparato = a.oid
                 and a.impianto = i.oid
                    -- and i.edificio = e.oid
                    -------------------
                 and i.edificio = OidEdificio ------2342-------------------------########################
                 and e.oid = OidEdificio -------2342------------------------########################
                    ---------------
                 and so.oid = regrdl.ultimostatooperativo) regrdlultimo,
             
             (select ar.risorseteam, count(ar.rowid) as nr_risorse
                from assrisorseteam ar
               group by ar.risorseteam) ar,
               
             (select kk.edificio,
                     kk.RISORSACAPO,
                     kk.centrooperativo,
                     kk.url,
                     CASE
                       WHEN DISTANZA = 0 then
                        'Non Calcolabile '
                       WHEN DISTANZA > 0 and DISTANZA <= 25 THEN
                        'in Prossimità'
                       WHEN DISTANZA > 25 and DISTANZA <= 50 THEN
                        'Vicino'
                       WHEN DISTANZA > 50 THEN
                        'Lontano'
                       ELSE
                        'Non Calcolabile'
                     END as DISTANZA
              
                from (select RISORSACAPO,
                             EDIFICIO,
                             centrooperativo,
                             'http://www.google.com/maps/place/' ||
                             Replace(latitudine_edificio, ',', '.') || ',' ||
                             Replace(longitudine_edificio, ',', '.') || '/@' ||
                             Replace(latitudine_edificio, ',', '.') || ',' ||
                             Replace(longitudine_edificio, ',', '.') || ',' ||
                             To_char(16) as url,
                             acos(round(sin(round(longitudine_edificio, 5)) *
                                        sin(round(longitudine_risorsa, 5)) +
                                        Cos(round(longitudine_edificio, 5)) *
                                        cos(round(longitudine_risorsa, 5)) *
                                        cos(round(latitudine_risorsa, 5) -
                                            round(latitudine_edificio, 5)),
                                        5)) * 6371 DISTANZA
                        from (select nvl(i.geolat, 41.904321) latitudine_edificio,
                                     nvl(i.geolng, 12.491455) longitudine_edificio,
                                     nvl(r.geolat, 41.904321) latitudine_risorsa,
                                     nvl(r.geolng, 12.491455) longitudine_risorsa,
                                     --t.Oid RISORSETEAM,
                                     e.oid       EDIFICIO,
                                     i.oid       INDIRIZZO,
                                     c.oid       as centrooperativo,
                                     risorsacapo RISORSACAPO
                                from edificio        e,
                                     indirizzo       i,
                                     risorse         r,
                                     risorseteam     t,
                                     centrooperativo c,
                                     commesse        cm
                               where e.indirizzo = i.oid
                                 and e.commessa = cm.oid
                                 and r.oid = t.risorsacapo
                                 and r.centrooperativo = c.oid
                                 and c.areadipolo = cm.areadipolo
                                 and e.cod_descrizione not in
                                     ('093',
                                      'PROVA_IARC',
                                      'PROVA_IARO',
                                  --    'LOCALITA-CRV-EDILE',
                                      'PROVA_IARL',
                                      '38004-IARO')
                                 and t.risorsacapo not in
                                     (502,
                                      522,
                                      523,
                                      602,
                                      622,
                                      623,
                                      702,
                                      722,
                                      723)
                                    -----------------
                                 and e.oid = OidEdificio -----------2342--------------------########################
                              ----------------                           
                              --and r.securityuserid is not null
                              ) vv) kk) distedificio
      
       where co.oid = t.centrooperativo
            --and co.cod_descrizione='IARCC'
         and ri.risorsacapo = t.oid
         and m.oid = t.mansione
         and ri.oid = regrdlag.risorsateam(+)
         and ri.oid = regrdlsos.risorsateam(+)
         and ri.oid = regrdlemerg.risorsateam(+)
         and ri.oid = regrdlultimo.risorsateam(+)
      --   and t.securityuserid = u."Oid"(+)
         and NVL2(t.securityuserid, 3, 1) > IsSmartphone
         and ar.risorseteam = ri.oid
         and t.oid || t.centrooperativo =
             distedificio.RISORSACAPO || distedificio.centrooperativo
         and distedificio.EDIFICIO = OidEdificio ---------2342----------------------########################
         and ri.oid = Conduttore.risorsateam(+)
      
      ;
  
    IO_CURSOR := V_CURSOR;
  
  EXCEPTION
    WHEN OTHERS THEN
      oMessaggio := 'Fallito: ' || SQLERRM;
      RAISE;
    
  end RISORSE_EDIFICIO_RDL;
    
procedure REPORT_RDL
(
icodiciRDL   in varchar2,
oMessaggio  out varchar2,
IO_CURSOR   IN OUT T_CURSOR
) 
 is
  
 V_CURSOR     T_CURSOR;
 v_apparatomp number:=0;
 v_componenti varchar2(4000);
begin

/*APPARATOSOSTEGNO	
APPARATOMP	
VVCOMPONENTI	
APPARATOPADRE	
*/


for nn in 
(
select 
a.descrizione, a.apparatomp
from apparato a
where 
a.apparatomp
in (
select distinct t.oidapparato
 from v_rdl_list_report t, apparato a 
where 
a.descrizione=t.Apparato
and t.Codice
in (
select regexp_substr (icodiciRDL, '[^,]+',1, rownum) str 
from dual 
connect by level <= regexp_count (icodiciRDL, '[^,]+') 
)))
loop
 if  v_apparatomp<>nn.apparatomp 
  then
   v_componenti:=nn.descrizione;
         insert into 
         TAPPARATO_COMPONENTI
         (apparatomp ,
          componenti
         )
         values
         (nn.apparatomp,
          v_componenti
          );
         commit;
  else
    
    if length(v_componenti)<4000 then 
      v_componenti:=v_componenti||'-'||nn.descrizione;
     update TAPPARATO_COMPONENTI ac
    set ac.componenti=ac.componenti||'-'||nn.descrizione
    where ac.apparatomp=nn.apparatomp;
    commit;  
    end if;  
   
 end if;    
 v_apparatomp:=nn.apparatomp;
end loop;  



OPEN V_CURSOR FOR 
select 
nvl(v.componenti,'NA') as ComponentiManutenzione,
t.componente2||t.componente1 as CorpoMP,
t.componente3 as OrdinePasso,
t.* from 
(
select
to_Char(t.oid)  as Codice,
--------
t.oid as CodiceRdL,
       e.descrizione as Edificio,
       -----
       e.cod_descrizione  as  CodEdificio,
       ----
       arp.descrizione as AreadiPolo,
       cm.centrocosto,
       i.descrizione as Impianto,
       a.descrizione as Apparato,
       ap.descrizione || '(' || ap.cod_uni || ')' as TipoApparato,
         NVL(prac.descrizione, 'NA') as problema,
         NVL(cau.descrizione, 'NA') as causa,
         NVL(rim.descrizione, 'NA') as rimedio,
       c.descrizione as CategoriaManutenzione,
       t.descrizione,
       rr.descrizione as regrdldescrizione,
       -- t.utenteinserimento as Utente,
       case
         when t.utenteinserimento like 'cc%' then
          'Call Center'
         else
          t.utenteinserimento
       end as Utente,
       to_char(t.datacreazione, 'dd/mm/yyyy HH24:MI:SS') as datacreazione,
       to_char(t.datarichiesta, 'dd/mm/yyyy HH24:MI:SS') as datarichiesta,
            to_char(t.dataupdate, 'dd/mm/yyyy HH24:MI:SS') as dataaggiornamento,
      
       to_number(to_char(t.datarichiesta, 'WW')) as settimana,
       to_number(to_char(t.datarichiesta, 'MM')) as mese,
       to_number(to_char(t.datarichiesta, 'yyyy')) as anno,
 case
   when t.datapianificata is not null then
    to_char(t.datapianificata, 'dd/mm/yyyy HH24:MI:SS')
   else
    ' na'
 end as datapianificata,
 --t.dataassegnazioneodl dataassegnazione,
    case
   when t.dataassegnazioneodl is not null then
    to_char(t.dataassegnazioneodl, 'dd/mm/yyyy HH24:MI:SS')
   else
    ' na'
 end as dataassegnazione,
case
   when t.datacompletamento is not null then
    to_char(t.datacompletamento, 'dd/mm/yyyy HH24:MI:SS')
   else
    ' na'
 end as datacompletamento,
       /*t.datariavvio
       t.datafermo,*/
       -----
      nvl(rt.team,'NA') as team,
      nvl( ms. descrizione,'NA') as TeamMansione,
       -------
       sm.statosmistamento,
       so.codstato as  statooperativo,
      NVL(t.notecompletamento, 'NA') AS notecompletamento,

       case
         when ri.telefono is not null then
          ri.nomecognome || '(' || tir.descrizione || ',' || ri.telefono || ')'
         else
          ri.nomecognome || '(' || tir.descrizione || ')'
       end as Richiedente,
       p.descrizione as Priorita,
       tp.descrizione as PrioritaTipoIntervento,
       amm.denominazione as refamministrativo,

       NVL(t.odl, 0) as CodiceOdL,
       t.regrdl as CodRegRdL,

       e.oid              as OidEdificio,
       cm.referentecofely as oidreferentecofely,
       t.categoria        as oidcategoria,
       sm.oid             as OIDSMISTAMENTO,
       

  CASE t.categoria
  WHEN 4 THEN ' '
  WHEN 1 THEN 'Scheda Mp:' || rmd.codschedemp ||' - Cod Scheda Mp UNI:' || rmd.cod_uni  ||' - Frequenza:'|| rmd.frequenza
  WHEN 5 THEN 'Scheda Mp:' ||rma.codschedemp  ||' - Cod Scheda Mp UNI:' || rma.cod_uni  ||' - Frequenza:'|| rma.frequenza
  ELSE ' '
  end as componente1,     
  
  CASE t.categoria  
  WHEN 4 THEN 'Problema:'|| prac.descrizione||' Causa: '||cau.descrizione
  WHEN 1 THEN ' '
  WHEN 5 THEN ' '
  ELSE ' '
  end as componente2, 
  
  CASE t.categoria
  WHEN 4 THEN ' '
  WHEN 1 THEN 'Nr:'||rmd.nordine||' - Passo '||rmd.passoschedamp
  WHEN 5 THEN 'Nr:'||rma.nordine||' - Passo '||rma.passoschedamp 
  ELSE ' '
END componente3,

     
CASE t.categoria
  WHEN 1 THEN rmd.codschedemp
  WHEN 5 THEN rma.codschedemp
  ELSE ' '
END codschedemp,

CASE t.categoria
  WHEN 1 THEN rmd.cod_uni
  WHEN 5 THEN rma.cod_uni
  ELSE ' '
END CodSchedaMPUNI,


CASE t.categoria
  WHEN 1 THEN rmd.descrizionemanutenzione
  WHEN 5 THEN rma.descrizionemanutenzione
  ELSE ' '
END descrizionemanutenzione,


CASE t.categoria
  WHEN 1 THEN rmd.frequenza
  WHEN 5 THEN rma.frequenza
  ELSE ' '
END frequenzadescrizione,


CASE t.categoria
  WHEN 1 THEN rmd.codfrequenza
  WHEN 5 THEN rma.codfrequenza
  ELSE ' '
END frequenzacod_descrizione,


CASE t.categoria
  WHEN 1 THEN rmd.passoschedamp
  WHEN 5 THEN rma.passoschedamp
  ELSE ' '
END passoschedamp,


CASE t.categoria
  WHEN 1 THEN rmd.nordine
  WHEN 5 THEN rma.nordine
  ELSE 0
END nordine,

CASE t.categoria
  WHEN 1 THEN rmd.insourcing
  WHEN 5 THEN rma.insourcing
  ELSE 0
END insourcing,
    --   a.keyplan,

      nvl(a.apparatomp,0),
      
      case when a.apparatopadre is not null
       then 
      (select b.cod_descrizione from apparato b where b.oid=a.apparatopadre)
      else
       'NA'
      
      end as apparatopadre
      ,
       case when a.apparatosostegno is not null
         then
       (select b.cod_descrizione from apparato b where b.oid=a.apparatosostegno)
       else
       'NA'
      end as APPARATOSOSTEGNO,
      
      a.oid as oidapparato
 
  from RDL t,
  Regrdl rr,
  mansioni ms,
       edificio e,
       commesse cm,
       areadipolo arp,
       impianto i,
       priorita p,
       tipointervento tp,
       Apparato a,
       categoria c,
       statosmistamento sm,
       statooperativo so,
       apparatostd ap,
       richiedente ri,
       CLIENTICONTATTI amm,
       richiedentetipo tir,
       --apparato apadre,
       --apparato asostegno,
       (select pr.descrizione, pra.oid
          from pcrappproblema pra, pcrproblemi pr
         where pr.oid = pra.pcrproblemi) prac,   ---  problemi

       (select ca.descrizione, pca.oid
          from pcrprobcausa pca, pcrcause ca
         where ca.oid = pca.pcrcause) cau,     --- causa

       (select ri.descrizione, pri.oid
          from pcrcausarimedio pri, pcrrimedi ri
         where ri.oid = pri.pcrrimedi) rim,        --- rimedio

       (select rt.oid,
               r.cognome || ' ' || r.nome || '(' || rt.anno || ')' as Team,
               r.oid as OidRisorse,
               r.mansione as RisorsaMansione
          from risorseteam rt, risorse r
         where r.oid = rt.risorsacapo) rt,  ------------   manutentore
                       ----------------   shede manutenzione programmata -------------------

       ( select rsa.rdl, rsa.apparatoschedamp, am.schedamp,
         am.descrizionemanutenzione, am.codschedemp, am.cod_uni,
          smp.passoschedamp, smp.nordine,
          f.cod_descrizione as codfrequenza, f.descrizione as frequenza, am.insourcing
           from rdlapparatoschedemp rsa , apparatoschedamp am, schedemp sm,
           schedemppassi smp, frequenze f
           where
           am.oid=rsa.apparatoschedamp
           and am.schedamp=sm.oid
           and f.oid=am.frequenzaopt
           and smp.schedemp=sm.oid) rma,

       ( select mpd.rdl, mp.apparatoschedamp, am.schedamp,
         am.descrizionemanutenzione, am.codschedemp, am.cod_uni,
         smp.passoschedamp, smp.nordine,
           f.cod_descrizione as codfrequenza, f.descrizione as frequenza, am.insourcing
           from mpattivitapianificatedett mpd ,mpattivitapianificate mp,
            apparatoschedamp am, schedemp sm,
            schedemppassi smp, frequenze f
           where
           mpd.mpattivitapianificate=mp.oid
           and am.oid=mp.apparatoschedamp
           and am.schedamp=sm.oid
           and f.oid=am.frequenzaopt
           and smp.schedemp=sm.oid
           ) rmd



 where e.oid = t.edificio
   and cm.oid = e.commessa
   and p.oid = t.priorita
   and tp.oid = t.tipointervento
   and i.oid = t.impianto
   and a.oid = t.apparato
   and c.oid = t.categoria
   and arp.oid = cm.areadipolo
   and rt.oid(+) = t.risorsateam
   and sm.oid = t.statosmistamento
   and so.oid(+) = t.statooperativo
   and ri.oid = t.richiedente
   and ap.oid = a.stdapparato
   and prac.oid(+) = t.pcrappproblema
   and cau.oid(+) = t.pcrprobcausa
   and rim.oid(+) = t.pcrcausarimedio
   and t.oid=rma.rdl(+) and   t.oid=rmd.rdl(+)
   --and man.rdl(+) = t.oid
   and tir.oid = ri.tiporichiedente
      --and t.oid = 1554
      --and o.regrdl(+)=t.regrdl
   and amm.oid(+) = cm.clienticontatti
   --
   and t.regrdl = rr.oid
   and ms.oid = rt.RisorsaMansione
   and to_Char(t.oid) in
          (select regexp_substr(icodiciRDL, '[^,]+', 1, rownum) str
                from dual
              connect by level <= regexp_count(icodiciRDL, '[^,]+'))
)
 t, TAPPARATO_COMPONENTI v
where v.apparatomp(+)=t.oidapparato
; 
IO_CURSOR := V_CURSOR;
  
  EXCEPTION
    WHEN OTHERS THEN
      oMessaggio := 'Fallito: ' || SQLERRM;
      RAISE;
    
  end REPORT_RDL;
  

    
procedure CALCOLO_COMPONENTE
(
icodiciRDL   in varchar2,
oMessaggio  out varchar2,
IO_CURSOR   IN OUT T_CURSOR
) 
 is
  
 V_CURSOR     T_CURSOR;
 v_apparatomp number:=0;
 v_componenti varchar2(4000);
begin

for nn in 
(
select 
a.descrizione, a.apparatomp
from apparato a
where 
a.apparatomp
in (
select distinct a.oid from v_rdl_list_report t, apparato a 
where 
a.descrizione=t.Apparato
and t.Codice
in (
select regexp_substr (icodiciRDL, '[^,]+',1, rownum) str 
from dual 
connect by level <= regexp_count (icodiciRDL, '[^,]+') 
)))
loop
 if  v_apparatomp<>nn.apparatomp 
  then
   v_componenti:=nn.descrizione;
         insert into 
         TAPPARATO_COMPONENTI
         (apparatomp ,
          componenti
         )
         values
         (nn.apparatomp,
          v_componenti
          );
         commit;
  else
    
    if length(v_componenti)<4000 then 
      v_componenti:=v_componenti||'-'||nn.descrizione;
     update TAPPARATO_COMPONENTI ac
    set ac.componenti=ac.componenti||'-'||nn.descrizione
    where ac.apparatomp=nn.apparatomp;
    commit;  
    end if;  
   
 end if;    
 v_apparatomp:=nn.apparatomp;
end loop;  



OPEN V_CURSOR FOR 
select 
v.componenti, v.apparatomp from  TAPPARATO_COMPONENTI v
; 
IO_CURSOR := V_CURSOR;
  
  EXCEPTION
    WHEN OTHERS THEN
      oMessaggio := 'Fallito: ' || SQLERRM;
      RAISE;
    
  end CALCOLO_COMPONENTE;
  
  
  
  
  
end PK_DC_SALAOPERATIVA;
/
