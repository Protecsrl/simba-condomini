--0) ##################################################
--------- POPOLARE LO STATO AUTORIZZATIVO
  0 Nuovo Intervento  L0  0   
  1 in Attesa di Dich. Tecnico  L1  0   
  2 in Attesa Accettazione SO L2  0   
  3 in Attesa di Trasferimento  L3  0   
  4 Intervento in Lavorazione L4  0   

/*                                             da       a
quando creo intervemto                        null      0
quando creo la notifica                       0         1
quando il tecnico dichiara orario             1         2
quando la SO esegue l'accettazione            2         3
quando il tecnico esegue la presa in carico   3         4
*/


--1) ##################################################
-- AGGIORNARE RISORSE TEAM DI NOTIFICHE RDL
select t.*, t.rowid from NOTIFICA_SK_RISORSE t;
--aggiungere in NOTIFICA_SK_RISORSE le risorseteam 
--            nel campo risorseteam, mentre nel oid la sequanza propria 
select t.*, t.rowid from risorseteam t
where t.oid not in (
select nrt.risorsateam from NOTIFICA_SK_RISORSE nrt
);
select t.*, t.rowid from NOTIFICA_SK_RISORSE t;
update NOTIFICA_SK_RISORSE nr set nr.risorsateam= ...

-- aggiornare oid delle risorse team
 select "sq_NOTIFICA_SK_RISORSE".nextval from dual  ---  1151
 commit;

--2) ##################################################
-- AGGIORNARE LE COMMESSE----- 
select * from COMMESSE t
/*M_LIVELLOAUTORIZZATIVOGUASTO	4	 -- il totale livelli di autorizzazione
M_BLOCCOLIVELLOAUTORIZZGUASTO	1	  -- vale come 1=treu, 0 = false ( blocca sull App la presa in carico)
M_TEMPOLIVELLOAUTORIZZOGUASTO	30;30;30;35	 -- sono i tempi di ritardo degli avvisi
M_LIV_AUTORIZ_PRESACARICO	2	  -- il livello che consente di prendere in carico ( blocca sull App la presa in carico)
*/
update commesse c 
set c.M_LIVELLOAUTORIZZATIVOGUASTO  4   -- il totale livelli di autorizzazione
,c.M_BLOCCOLIVELLOAUTORIZZGUASTO = 1    -- vale come 1=treu, 0 = false ( blocca sull App la presa in carico)
,c.M_TEMPOLIVELLOAUTORIZZOGUASTO ='30;30;30;35'   -- sono i tempi di ritardo degli avvisi
,c.M_LIV_AUTORIZ_PRESACARICO=  2    -- il livello che consente di prendere in carico ( blocca sull App la presa in carico)

--3) ##################################################
---  aggiornare RDL LA DATA PIANIFICATA E ALTRE NUOVE (è nuova)
select 
rdl.datapianificata,       -- DOVREBBE ESSRRE GIà DEVERSA DA NULL
rdl.datapianificataend     -- IMPOSTARE CON DATAPIANIFICATA + 60 MINUTI (DI DEFAULT VECCHI)
,rdl.datacompletamentosys --- IMPOSTARE CO LA DATA DI COMPLETAMENTO DA SISTEMA
, rdl.autorizzazione      --- PER LE COMMESSE NON SOGGETTE AD AUTORIZZAZIONE IMPOSTARE = 0
                            --- PER LE COMMESSE SOGGETTE A SATATO AUTORIZZATIVO
                            ---  SE SMISTAMENTO = 1  SATATOAUTORIZZATIVO = 0; SM=2->1; SM=3->ULTIMOSA; SM=4 -> ULTIMO SA
                              
from rdl ;


--4) ##################################################
---  aggiornre la procedura di registro smistamento e operaivo
-- il file si trova su repositori scaricando da assembla nella seguente cartella
--C:\AssemblaPRT17\EAMS\CAMS.Module\SPDB\PK_AuditData.
--CREATE OR REPLACE PACKAGE PK_AuditData is   ...........................
begin
  -- Call the procedure
  pk_auditdata.getauditdata(ioidobj => :ioidobj,
                            itypeobj => :itypeobj,
                            idatalimite => :idatalimite,
                            iusername => :iusername,
                            omessaggio => :omessaggio,
                            io_cursor => :io_cursor);
end;

--5) ##################################################
---  AGGIORNARE LA EX PROCEDURA DI REGISTRO SMISTAMENTO
-- NELLA PROCEDURA INSERIRE UN RETURN ALL'INIZIO
/*procedure SP_GESTIONESTATOOPERATIVOODL (
                                 p_azione in varchar2,
                                 p_regrdl_old in number,
                                ..........................
                                 p_msg_out out varchar2)
                                 is
  p_data_completamento_v varchar2(20):=p_data_completamento;
  p_ora_completamento_v varchar2(10):= p_ora_completamento;
.................
  v_err_msg   varchar2(250);
  BEGIN*/
    ---  DA QUI INSERIRE
p_msg_out:='';
p_stdisprisorsa:=0;
RETURN;   ----   INSERIRE QUESTO 
    --- FINO A QUI 
insert into tbl_sql_mobile
  (s_sql, data)

--6) ##################################################
---  AGGIORNARE LA PROCEDURA DI ACCESSO DELL'APP (ALTRIMENTI NON  PARTE AVANZAMENTO LAVORI)
 -- il file si trova su repositori scaricando da assembla nella seguente cartella
--C:\AssemblaPRT17\EAMS\CAMS.Module\SPDB\PK_Mobile_accont.
PK_Mobile_accont


--7) ##################################################
---  AGGIORNARE LO STRICO DELLE POSIZIONI
 -- il file si trova su repositori scaricando da assembla nella seguente cartella
--C:\AssemblaPRT17\EAMS\CAMS.Module\SPDB\PK_Mobile_accont.
 select * from risorse t where t.oid = 907;
--select * from risorseteam t where t.oid = 569;
select * from risorse t ;
--select * from risorseteam t ;
 select '[Int32]''' || '48137' || '''' as mio,
             t."OID",
             t."TypeName",
             r."TargetKey" as RDL_CODICE,
             a."Oid",
             a."OperationType",
             a."UserName" as UTENTESO,
             a."ModifiedOn" as DATAORA,
            Nvl( a."PropertyName",'nd') || '-' || to_char(a."Description") as DESCRIZIONE,
            Nvl( a."OldObject",'nd') as RISORSETEAM_DESC_OLD,
           Nvl(  a."NewObject",'nd') as RISORSETEAM_DESC,
          Nvl(   a."OldValue",'nd') as SSMISTAMENTO_DESC,
            Nvl( a."NewValue",'nd') as SSMISTAMENTO_DESC_OLD,
           Nvl(  a."OldValue",'nd') as SOPERATIVO_DESC,
           Nvl(  a."NewValue",'nd') as SOPERATIVO_DESC_OLD,
             a."ModifiedOn" as DATAPIANIFICATA,
             a."ModifiedOn" as DATAPIANIFICATA_OLD,
          Nvl(   a."PropertyName",'nd') as PropertyName
        from "AuditDataItemPersistent" a,
             "XPWeakReference"         r,
             "XPObjectType"            t
       where r."TargetType" = t."OID"
         and a."AuditedObject" = r."Oid"
         and t."OID" = 256 --iTypeObj -- TypeObj --25 = RDL;
            --   and t."TargetType" = 25 -----25 = RDL;
      --   and r."TargetKey" = '[Int32]''' || iOidObj || '''' -- '[Int32]'''||'48137'||'''' --    and r."TargetKey" like '%48137%' -- TargetKey 
         and a."ModifiedOn" > sysdate - 1
      --and to_char(a."Description") like '%26043%'
       order by a."ModifiedOn" desc;
       
--	256	CAMS.Module.DBTask.Risorse	CAMS.Module
--	139	CAMS.Module.DBTask.RisorseTeam	CAMS.Module

--8) ##################################################
---  AGGIORNARE IL VALORE DEL TEMPO RELATIVO ALL'INTERVENTO NEL PROBLEMA
 
UPDATE  PCRPROBLEMI t SET T.VALORE = 60 -- MINUTI

--9) ##############################################à
---inserire i seguenti privilegi a utente
Base Task
Scheduler Event
Resource
Task
Stato Autorizzativo
Log di Sistema
GeoLocalizzazione
Strade
Sfoglia Apparato
Notifiche RdL
Agenda Risorse

 
-- 10 ) ##############################################à
 --INIBIRE LA SEGUENTE STORE PROCEDURE

--inibire questa store procedure mettendo return subito dopo entrata
-- OracleCommand("pk_mpasscaricocapacita.regrdlemergenzadaco", OrclConn) 
RETURN;


-- INFO) ##################################################
--- create table ModuleInfo_old as, t.rowid 
/*select t.* from "ModuleInfo" t
delete  "ModuleInfo" 
select t.*, t.rowid from ModuleInfo_old t*/



--aggiornare pack PK_DC_SALAOPERATIVA  con store procedure  REPORT_RDL e CALCOLO_COMPONENTE


--procedure REPORT_RDL
--(
--icodiciRDL   in varchar2,
--oMessaggio  out varchar2,
--IO_CURSOR   IN OUT T_CURSOR
--) 
-- is
  
-- V_CURSOR     T_CURSOR;
-- v_apparatomp number:=0;
-- v_componenti varchar2(4000);
--begin


--insert into tbl_sql
--(tbl_sql.s_sql, 
--tbl_sql.data)
--values('REPORT_RDL input icodiciRDL: '||icodiciRDL,sysdate);
--commit;

--/*APPARATOSOSTEGNO	
--APPARATOMP	
--VVCOMPONENTI	
--APPARATOPADRE	
--*/


--for nn in 
--(
--select 
--a.descrizione, a.apparatomp
--from apparato a
--where 
--a.apparatomp
--in (
--select distinct t.oidapparato
-- from (
-- select
--to_Char(t.oid)  as Codice,
--a.descrizione as apparato,
--a.oid as oidapparato
 
--  from RDL t,
--  Regrdl rr,
--  --mansioni ms,
--       edificio e,
--       commesse cm,
--       areadipolo arp,
--       impianto i,
--       priorita p,
--       tipointervento tp,
--       Apparato a,
--       categoria c,
--       statosmistamento sm,
--       statooperativo so,
--       apparatostd ap,
--       richiedente ri,
--       CLIENTICONTATTI amm,
--       richiedentetipo tir,
--       --apparato apadre,
--       --apparato asostegno,
--       (select pr.descrizione, pra.oid
--          from pcrappproblema pra, pcrproblemi pr
--         where pr.oid = pra.pcrproblemi) prac,   ---  problemi

--       (select ca.descrizione, pca.oid
--          from pcrprobcausa pca, pcrcause ca
--         where ca.oid = pca.pcrcause) cau,     --- causa

--       (select ri.descrizione, pri.oid
--          from pcrcausarimedio pri, pcrrimedi ri
--         where ri.oid = pri.pcrrimedi) rim,        --- rimedio

--       (select rt.oid,
--               r.cognome || ' ' || r.nome || '(' || rt.anno || ')' as Team,
--               r.oid as OidRisorse,
--               m.descrizione as Mansione,
--               r.mansione as RisorsaMansione
--          from risorseteam rt, risorse r, mansioni m
--         where r.oid = rt.risorsacapo
--         and m.oid=r.mansione) rt,  ------------   manutentore
--                       ----------------   shede manutenzione programmata -------------------

--       ( select rsa.rdl, rsa.apparatoschedamp, am.schedamp,
--         am.descrizionemanutenzione, am.codschedemp, am.cod_uni,
--          smp.passoschedamp, smp.nordine,
--          f.cod_descrizione as codfrequenza, f.descrizione as frequenza, am.insourcing
--           from rdlapparatoschedemp rsa , apparatoschedamp am, schedemp sm,
--           schedemppassi smp, frequenze f
--           where
--           am.oid=rsa.apparatoschedamp
--           and am.schedamp=sm.oid
--           and f.oid=am.frequenzaopt
--           and smp.schedemp=sm.oid) rma,

--       ( select mpd.rdl, mp.apparatoschedamp, am.schedamp,
--         am.descrizionemanutenzione, am.codschedemp, am.cod_uni,
--         smp.passoschedamp, smp.nordine,
--           f.cod_descrizione as codfrequenza, f.descrizione as frequenza, am.insourcing
--           from mpattivitapianificatedett mpd ,mpattivitapianificate mp,
--            apparatoschedamp am, schedemp sm,
--            schedemppassi smp, frequenze f
--           where
--           mpd.mpattivitapianificate=mp.oid
--           and am.oid=mp.apparatoschedamp
--           and am.schedamp=sm.oid
--           and f.oid=am.frequenzaopt
--           and smp.schedemp=sm.oid
--           ) rmd



-- where e.oid = t.edificio
--   and cm.oid = e.commessa
--   and p.oid = t.priorita
--   and tp.oid = t.tipointervento
--   and i.oid = t.impianto
--   and a.oid = t.apparato
--   and c.oid = t.categoria
--   and arp.oid = cm.areadipolo
--   and rt.oid(+) = t.risorsateam
--   and sm.oid = t.statosmistamento
--   and so.oid(+) = t.statooperativo
--   and ri.oid = t.richiedente
--   and ap.oid = a.stdapparato
--   and prac.oid(+) = t.pcrappproblema
--   and cau.oid(+) = t.pcrprobcausa
--   and rim.oid(+) = t.pcrcausarimedio
--   and t.oid=rma.rdl(+) 
--   and t.oid=rmd.rdl(+)
--   --and man.rdl(+) = t.oid
--   and tir.oid = ri.tiporichiedente
--      --and t.oid = 1554
--      --and o.regrdl(+)=t.regrdl
--   and amm.oid(+) = cm.clienticontatti
--   --
--   and t.regrdl = rr.oid
--   --and ms.oid = rt.RisorsaMansione
--   and to_Char(t.oid) in
--          (select regexp_substr('29004,29005,', '[^,]+', 1, rownum) str
--                from dual
--              connect by level <= regexp_count('29004,29005,', '[^,]+'))
-- )
 
-- t, apparato a 
--where 
--a.descrizione=t.Apparato
--and t.Codice
--in (
--select regexp_substr (icodiciRDL, '[^,]+',1, rownum) str 
--from dual 
--connect by level <= regexp_count (icodiciRDL, '[^,]+') 
--)))
--loop
-- if  v_apparatomp<>nn.apparatomp 
--  then
--   v_componenti:=nn.descrizione;
--         insert into 
--         TAPPARATO_COMPONENTI
--         (apparatomp ,
--          componenti
--         )
--         values
--         (nn.apparatomp,
--          v_componenti
--          );
--         commit;
--  else
    
--    if length(v_componenti)<4000 then 
--      v_componenti:=v_componenti||'-'||nn.descrizione;
--     update TAPPARATO_COMPONENTI ac
--    set ac.componenti=ac.componenti||'-'||nn.descrizione
--    where ac.apparatomp=nn.apparatomp;
--    commit;  
--    end if;  
   
-- end if;    
-- v_apparatomp:=nn.apparatomp;
--end loop;  



--OPEN V_CURSOR FOR 
--select 
--nvl(v.componenti,'NA') as ComponentiManutenzione,
--t.componente2||t.componente1 as CorpoMP,
--t.componente3 as OrdinePasso,
--t.* from 
--(
--select
--to_Char(t.oid)  as Codice,
----------
--t.oid as CodiceRdL,
--       e.descrizione as Edificio,
--       -----
--       e.cod_descrizione  as  CodEdificio,
--       ----
--       arp.descrizione as AreadiPolo,
--       cm.centrocosto,
--       i.descrizione as Impianto,
--       a.descrizione as Apparato,
--       ap.descrizione || '(' || ap.cod_uni || ')' as TipoApparato,
--         NVL(prac.descrizione, 'NA') as problema,
--         NVL(cau.descrizione, 'NA') as causa,
--         NVL(rim.descrizione, 'NA') as rimedio,
--       c.descrizione as CategoriaManutenzione,
--       t.descrizione,
--       rr.descrizione as regrdldescrizione,
--       -- t.utenteinserimento as Utente,
--       case
--         when t.utenteinserimento like 'cc%' then
--          'Call Center'
--         else
--          t.utenteinserimento
--       end as Utente,
--       to_char(t.datacreazione, 'dd/mm/yyyy HH24:MI:SS') as datacreazione,
--       to_char(t.datarichiesta, 'dd/mm/yyyy HH24:MI:SS') as datarichiesta,
--            to_char(t.dataupdate, 'dd/mm/yyyy HH24:MI:SS') as dataaggiornamento,
      
--       to_number(to_char(t.datarichiesta, 'WW')) as settimana,
--       to_number(to_char(t.datarichiesta, 'MM')) as mese,
--       to_number(to_char(t.datarichiesta, 'yyyy')) as anno,
-- case
--   when t.datapianificata is not null then
--    to_char(t.datapianificata, 'dd/mm/yyyy HH24:MI:SS')
--   else
--    ' na'
-- end as datapianificata,
-- --t.dataassegnazioneodl dataassegnazione,
--    case
--   when t.dataassegnazioneodl is not null then
--    to_char(t.dataassegnazioneodl, 'dd/mm/yyyy HH24:MI:SS')
--   else
--    ' na'
-- end as dataassegnazione,
--case
--   when t.datacompletamento is not null then
--    to_char(t.datacompletamento, 'dd/mm/yyyy HH24:MI:SS')
--   else
--    ' na'
-- end as datacompletamento,
--       /*t.datariavvio
--       t.datafermo,*/
--       -----
--      nvl(rt.team,'NA') as team,
--      nvl( rt.mansione,'NA') as TeamMansione,
--       -------
--       sm.statosmistamento,
--       nvl(so.codstato,'NA') as  statooperativo,
--      NVL(t.notecompletamento, 'NA') AS notecompletamento,

--       case
--         when ri.telefono is not null then
--          ri.nomecognome || '(' || tir.descrizione || ',' || ri.telefono || ')'
--         else
--          ri.nomecognome || '(' || tir.descrizione || ')'
--       end as Richiedente,
--       p.descrizione as Priorita,
--       tp.descrizione as PrioritaTipoIntervento,
--       amm.denominazione as refamministrativo,

--       NVL(t.odl, 0) as CodiceOdL,
--       t.regrdl as CodRegRdL,

--       e.oid              as OidEdificio,
--       cm.referentecofely as oidreferentecofely,
--       t.categoria        as oidcategoria,
--       sm.oid             as OIDSMISTAMENTO,
       

--  CASE t.categoria
--  WHEN 4 THEN ' '
--  WHEN 1 THEN 'Scheda Mp:' || rmd.codschedemp ||' - Cod Scheda Mp UNI:' || rmd.cod_uni  ||' - Frequenza:'|| rmd.frequenza
--  WHEN 5 THEN 'Scheda Mp:' ||rma.codschedemp  ||' - Cod Scheda Mp UNI:' || rma.cod_uni  ||' - Frequenza:'|| rma.frequenza
--  ELSE ' '
--  end as componente1,     
  
--  CASE t.categoria  
--  WHEN 4 THEN 'Problema:'|| prac.descrizione||' Causa: '||cau.descrizione
--  WHEN 1 THEN ' '
--  WHEN 5 THEN ' '
--  ELSE ' '
--  end as componente2, 
  
--  CASE t.categoria
--  WHEN 4 THEN ' '
--  WHEN 1 THEN 'Nr:'||rmd.nordine||' - Passo '||rmd.passoschedamp
--  WHEN 5 THEN 'Nr:'||rma.nordine||' - Passo '||rma.passoschedamp 
--  ELSE ' '
--END componente3,

     
--CASE t.categoria
--  WHEN 1 THEN rmd.codschedemp
--  WHEN 5 THEN rma.codschedemp
--  ELSE ' '
--END codschedemp,

--CASE t.categoria
--  WHEN 1 THEN rmd.cod_uni
--  WHEN 5 THEN rma.cod_uni
--  ELSE ' '
--END CodSchedaMPUNI,


--CASE t.categoria
--  WHEN 1 THEN rmd.descrizionemanutenzione
--  WHEN 5 THEN rma.descrizionemanutenzione
--  ELSE ' '
--END descrizionemanutenzione,


--CASE t.categoria
--  WHEN 1 THEN rmd.frequenza
--  WHEN 5 THEN rma.frequenza
--  ELSE ' '
--END frequenzadescrizione,


--CASE t.categoria
--  WHEN 1 THEN rmd.codfrequenza
--  WHEN 5 THEN rma.codfrequenza
--  ELSE ' '
--END frequenzacod_descrizione,


--CASE t.categoria
--  WHEN 1 THEN rmd.passoschedamp
--  WHEN 5 THEN rma.passoschedamp
--  ELSE ' '
--END passoschedamp,


--CASE t.categoria
--  WHEN 1 THEN rmd.nordine
--  WHEN 5 THEN rma.nordine
--  ELSE 0
--END nordine,

--CASE t.categoria
--  WHEN 1 THEN rmd.insourcing
--  WHEN 5 THEN rma.insourcing
--  ELSE 0
--END insourcing,
--    --   a.keyplan,

--      nvl(a.apparatomp,0),
      
--      case when a.apparatopadre is not null
--       then 
--      (select b.cod_descrizione from apparato b where b.oid=a.apparatopadre)
--      else
--       'NA'
      
--      end as apparatopadre
--      ,
--       case when a.apparatosostegno is not null
--         then
--       (select b.cod_descrizione from apparato b where b.oid=a.apparatosostegno)
--       else
--       'NA'
--      end as APPARATOSOSTEGNO,
      
--      a.oid as oidapparato
 
--  from RDL t,
--  Regrdl rr,
--  --mansioni ms,
--       edificio e,
--       commesse cm,
--       areadipolo arp,
--       impianto i,
--       priorita p,
--       tipointervento tp,
--       Apparato a,
--       categoria c,
--       statosmistamento sm,
--       statooperativo so,
--       apparatostd ap,
--       richiedente ri,
--       CLIENTICONTATTI amm,
--       richiedentetipo tir,
--       --apparato apadre,
--       --apparato asostegno,
--       (select pr.descrizione, pra.oid
--          from pcrappproblema pra, pcrproblemi pr
--         where pr.oid = pra.pcrproblemi) prac,   ---  problemi

--       (select ca.descrizione, pca.oid
--          from pcrprobcausa pca, pcrcause ca
--         where ca.oid = pca.pcrcause) cau,     --- causa

--       (select ri.descrizione, pri.oid
--          from pcrcausarimedio pri, pcrrimedi ri
--         where ri.oid = pri.pcrrimedi) rim,        --- rimedio

--       (select rt.oid,
--               r.cognome || ' ' || r.nome || '(' || rt.anno || ')' as Team,
--               r.oid as OidRisorse,
--               m.descrizione as Mansione,
--               r.mansione as RisorsaMansione
--          from risorseteam rt, risorse r, mansioni m
--         where r.oid = rt.risorsacapo
--         and m.oid=r.mansione) rt,  ------------   manutentore
--                       ----------------   shede manutenzione programmata -------------------

--       ( select rsa.rdl, rsa.apparatoschedamp, am.schedamp,
--         am.descrizionemanutenzione, am.codschedemp, am.cod_uni,
--          smp.passoschedamp, smp.nordine,
--          f.cod_descrizione as codfrequenza, f.descrizione as frequenza, am.insourcing
--           from rdlapparatoschedemp rsa , apparatoschedamp am, schedemp sm,
--           schedemppassi smp, frequenze f
--           where
--           am.oid=rsa.apparatoschedamp
--           and am.schedamp=sm.oid
--           and f.oid=am.frequenzaopt
--           and smp.schedemp=sm.oid) rma,

--       ( select mpd.rdl, mp.apparatoschedamp, am.schedamp,
--         am.descrizionemanutenzione, am.codschedemp, am.cod_uni,
--         smp.passoschedamp, smp.nordine,
--           f.cod_descrizione as codfrequenza, f.descrizione as frequenza, am.insourcing
--           from mpattivitapianificatedett mpd ,mpattivitapianificate mp,
--            apparatoschedamp am, schedemp sm,
--            schedemppassi smp, frequenze f
--           where
--           mpd.mpattivitapianificate=mp.oid
--           and am.oid=mp.apparatoschedamp
--           and am.schedamp=sm.oid
--           and f.oid=am.frequenzaopt
--           and smp.schedemp=sm.oid
--           ) rmd



-- where e.oid = t.edificio
--   and cm.oid = e.commessa
--   and p.oid = t.priorita
--   and tp.oid = t.tipointervento
--   and i.oid = t.impianto
--   and a.oid = t.apparato
--   and c.oid = t.categoria
--   and arp.oid = cm.areadipolo
--   and rt.oid(+) = t.risorsateam
--   and sm.oid = t.statosmistamento
--   and so.oid(+) = t.statooperativo
--   and ri.oid = t.richiedente
--   and ap.oid = a.stdapparato
--   and prac.oid(+) = t.pcrappproblema
--   and cau.oid(+) = t.pcrprobcausa
--   and rim.oid(+) = t.pcrcausarimedio
--   and t.oid=rma.rdl(+) 
--   and t.oid=rmd.rdl(+)
--   --and man.rdl(+) = t.oid
--   and tir.oid = ri.tiporichiedente
--      --and t.oid = 1554
--      --and o.regrdl(+)=t.regrdl
--   and amm.oid(+) = cm.clienticontatti
--   --
--   and t.regrdl = rr.oid
--   --and ms.oid = rt.RisorsaMansione
--   and to_Char(t.oid) in
--          (select regexp_substr(icodiciRDL, '[^,]+', 1, rownum) str
--                from dual
--              connect by level <= regexp_count(icodiciRDL, '[^,]+'))
--)
-- t, TAPPARATO_COMPONENTI v
--where v.apparatomp(+)=t.oidapparato
--; 
--IO_CURSOR := V_CURSOR;
  
--  EXCEPTION
--    WHEN OTHERS THEN
--      oMessaggio := 'Fallito: ' || SQLERRM;
--      RAISE;
    
--  end REPORT_RDL;
  

    
--procedure CALCOLO_COMPONENTE
--(
--icodiciRDL   in varchar2,
--oMessaggio  out varchar2,
--IO_CURSOR   IN OUT T_CURSOR
--) 
-- is
  
-- V_CURSOR     T_CURSOR;
-- v_apparatomp number:=0;
-- v_componenti varchar2(4000);
--begin

--for nn in 
--(
--select 
--a.descrizione, a.apparatomp
--from apparato a
--where 
--a.apparatomp
--in (
--select distinct a.oid from v_rdl_list_report t, apparato a 
--where 
--a.descrizione=t.Apparato
--and t.Codice
--in (
--select regexp_substr (icodiciRDL, '[^,]+',1, rownum) str 
--from dual 
--connect by level <= regexp_count (icodiciRDL, '[^,]+') 
--)))
--loop
-- if  v_apparatomp<>nn.apparatomp 
--  then
--   v_componenti:=nn.descrizione;
--         insert into 
--         TAPPARATO_COMPONENTI
--         (apparatomp ,
--          componenti
--         )
--         values
--         (nn.apparatomp,
--          v_componenti
--          );
--         commit;
--  else
    
--    if length(v_componenti)<4000 then 
--      v_componenti:=v_componenti||'-'||nn.descrizione;
--     update TAPPARATO_COMPONENTI ac
--    set ac.componenti=ac.componenti||'-'||nn.descrizione
--    where ac.apparatomp=nn.apparatomp;
--    commit;  
--    end if;  
   
-- end if;    
-- v_apparatomp:=nn.apparatomp;
--end loop;  



--OPEN V_CURSOR FOR 
--select 
--v.componenti, v.apparatomp from  TAPPARATO_COMPONENTI v
--; 
--IO_CURSOR := V_CURSOR;
  
--  EXCEPTION
--    WHEN OTHERS THEN
--      oMessaggio := 'Fallito: ' || SQLERRM;
--      RAISE;
    
--  end CALCOLO_COMPONENTE;
  
  
  