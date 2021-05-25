create or replace view v_rdl_list_report as
select case
         when man.AppskMP is not null then
          t.oid || '-' || man.AppskMP || '-' || man.nordine
         else
          to_Char(t.oid)
       end as Codice,
       --------
       t.oid         as CodiceRdL,
       e.descrizione as Edificio,
       -----
       e.oid as CodEdificio,
       ----
       arp.descrizione as AreadiPolo,
       cm.centrocosto,
       i.descrizione as Impianto,
       a.descrizione as Apparato,
       ap.descrizione || '(' || ap.cod_uni || ')' as TipoApparato,
       prac.descrizione as problema,
       cau.descrizione as causa,
       rim.descrizione as rimedio,
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
       t.datacreazione,
       t.datarichiesta,
       t.dataupdate dataaggiornamento,
       to_number(to_char(t.datarichiesta, 'WW')) as settimana,
       to_number(to_char(t.datarichiesta, 'MM')) as mese,
       to_number(to_char(t.datarichiesta, 'yyyy')) as anno,
       t.datapianificata,
       t.dataassegnazioneodl dataassegnazione,
       t.datacompletamento,
       /*t.datariavvio
       t.datafermo,*/
       -----
       rt.team,
       ms.     descrizione as TeamMansione,
       -------
       sm.statosmistamento,
       so.statooperativo,
       t.notecompletamento,
       case
         when ri.telefono is not null then
          ri.nomecognome || '(' || tir.descrizione || ',' || ri.telefono || ')'
         else
          ri.nomecognome || '(' || tir.descrizione || ')'
       end as Richiedente,
       p.descrizione as Priorita,
       tp.descrizione as PrioritaTipoIntervento,
       amm.denominazione as refamministrativo,
       
       t.odl    as CodiceOdL,
       t.regrdl as CodRegRdL,
       
       e.oid              as OidEdificio,
       cm.referentecofely as oidreferentecofely,
       t.categoria        as oidcategoria,
       sm.oid             as OIDSMISTAMENTO,
       --aggiunta manutenzione e passi
       man.codschedemp,
       man.CodSchedaMPUNI,
       man.descrizionemanutenzione,
       man.frequenzadescrizione,
       man.frequenzacod_descrizione,
       Nvl(man.nordine, 0) as nordine,
       man.passoschedamp,
       man.insourcing,
      nvl(   a.apparatosostegno,'NA') as apparatosostegno,
     nvl(  a.apparatopadre,'NA') as apparatopadre,
/*       nvl((select listagg(appmp.cod_descrizione, '; ') within group(order by appmp.oid) csv
             from apparato appmp
            where appmp.apparatomp = a.oid --so.oid
           ),
           'NA') as Componenti_Manutenzione,*/
       'na'  as Componenti_Manutenzione,
  /*     nvl((select listagg(appmp.cod_descrizione, '; ') within group(order by appmp.oid) csv
             from apparato appmp
            where appmp.apparatosostegno = a.oid --so.oid
           ),
           'NA') as Componenti_Sostegno*/
 'NA' as Componenti_Sostegno
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
       (select pr.descrizione, pra.oid
          from pcrappproblema pra, pcrproblemi pr
         where pr.oid = pra.pcrproblemi) prac,
       
       (select ca.descrizione, pca.oid
          from pcrprobcausa pca, pcrcause ca
         where ca.oid = pca.pcrcause) cau,
       
       (select ri.descrizione, pri.oid
          from pcrcausarimedio pri, pcrrimedi ri
         where ri.oid = pri.pcrrimedi) rim,
       
       (select rt.oid,
               r.cognome || ' ' || r.nome || '(' || rt.anno || ')' as Team,
               r.oid as OidRisorse,
               r.mansione as RisorsaMansione
          from risorseteam rt, risorse r
         where r.oid = rt.risorsacapo) rt,
       (select distinct t.rdl, --m.apparatoschedamp,
                        apsk.oid as AppskMP,
                        apsk.codschedemp,
                        apsk.cod_uni as CodSchedaMPUNI,
                        sm.descrizionemanutenzione,
                        f.descrizione as frequenzadescrizione,
                        f.cod_descrizione as frequenzacod_descrizione,
                        smp.nordine,
                        smp.passoschedamp,
                        decode(apsk.insourcing, 0, 'Si', 1, 'No', 'ND') insourcing
          from mpattivitapianificate     m,
               MPATTIVITAPIANIFICATEDETT t,
               apparatoschedamp          apsk,
               schedemp                  sm,
               frequenze                 f,
               schedemppassi             smp
         where m.oid = t.mpattivitapianificate
              --and t.rdl is not null
           and apsk.oid = m.apparatoschedamp
           and sm.oid = apsk.schedamp
           and f.oid = sm.frequenzaopt
           and smp.schedemp = sm.oid
        
        union
        
        select v.rdl,
               apsk.oid as AppskMP,
               apsk.codschedemp,
               apsk.cod_uni as CodSchedaMPUNI,
               sm.descrizionemanutenzione,
               f.descrizione as frequenzadescrizione,
               f.cod_descrizione as frequenzacod_descrizione,
               smp.nordine,
               smp.passoschedamp,
               decode(apsk.insourcing, 0, 'Si', 1, 'No', 'ND') insourcing
          from RDLAPPARATOSCHEDEMP v,
               apparatoschedamp    apsk,
               schedemp            sm,
               frequenze           f,
               schedemppassi       smp
         where v.apparatoschedamp = apsk.oid
           and sm.oid = apsk.schedamp
           and f.oid = sm.frequenzaopt
           and smp.schedemp = sm.oid
        
        ) man

--, (select o.oid,o.regrdl from odl o) o

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
   and man.rdl(+) = t.oid
   and tir.oid = ri.tiporichiedente
      --and t.oid = 1554
      --and o.regrdl(+)=t.regrdl
   and amm.oid(+) = cm.clienticontatti
      --
   and t.regrdl = rr.oid
   and ms.oid = rt.RisorsaMansione;
