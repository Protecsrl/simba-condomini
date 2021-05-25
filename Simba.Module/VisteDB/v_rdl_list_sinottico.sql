create or replace view v_rdl_list_sinottico as
select t.oid as Codice,
       e.descrizione as Edificio,
       cu.denominazioneitaliano as Comune,
       arp.descrizione as AreadiPolo,
       cm.centrocosto  ,
       i.descrizione as Impianto,
       a.descrizione as Apparato,
       ap.descrizione || '(' || ap.cod_uni || ')' as TipoApparato,
       prac.descrizione as problema,
       cau.descrizione as causa,
       rim.descrizione as rimedio,
       c.descrizione as CategoriaManutenzione,
      -- t.descrizione,

   /*   case when sr.rdl is not null
       then t.descrizione ||' (Solleciti nr:'||sr.nrsolleciti||')'
       else t.descrizione
       end  as  descrizione,*/

              case when length(t.descrizione) > 500 then
        substr( t.descrizione ,1,497) || '...'
        else
          t.descrizione
          end
        as descrizione ,



      -- t.utenteinserimento as Utente,
       case when
       t.utenteinserimento like 'cc%' then
       'Call Center'
       else
           t.utenteinserimento
       end   as Utente,
       t.datacreazione,
       t.datarichiesta,
       t.dataupdate dataaggiornamento,
       --NUOVI CAMPI
       t.data_sopralluogo,
       t.reparto,
       case when t.locali is not null
         then l.piano
          else ''
       end piano,
       case when t.locali is not null
         then l.stanza
          else ''
       end stanza,

    /*   to_number(to_char(t.datarichiesta, 'IW')) as settimana,
       to_number(to_char(t.datarichiesta, 'MM')) as mese,
       to_number(to_char(t.datarichiesta, 'yyyy')) as anno,*/

             case when t.categoria=5 or t.categoria=1
       then
       to_number(to_char(t.datapianificata, 'IW'))
       else to_number(to_char(t.datarichiesta, 'IW'))
       end as settimana,

       case when t.categoria=5 or t.categoria=1
       then
       to_number(to_char(t.datapianificata, 'MM'))
       else to_number(to_char(t.datarichiesta, 'MM'))
       end as mese,

       case when t.categoria=5 or t.categoria=1
       then
       to_number(to_char(t.datapianificata, 'yyyy'))
       else to_number(to_char(t.datarichiesta, 'yyyy'))
       end as anno,




       t.datapianificata,
       t.dataassegnazioneodl dataassegnazione,
       t.datacompletamento,
       /*t.datariavvio
       t.datafermo,*/
       rt.team,
       sm.statosmistamento,
       so.statooperativo,
       t.notecompletamento,
       case when ri.telefono is not null
       then ri.nomecognome ||'('||tir.descrizione||','||ri.telefono ||')'
       else
        ri.nomecognome ||'('||tir.descrizione||')'
        end  as Richiedente,
       case when sr.rdl is not null
       then 'SI'
       else 'NO'
        end  as Solleciti,


       p.descrizione       as Priorita,
       tp.descrizione      as PrioritaTipoIntervento,
       amm.denominazione   as refamministrativo,
---
--------------
      Floor( (sysdate - Nvl(t.datapianificata ,sysdate))* 1440 ) as minuti_passati ,
--------------   (1)	In Gestione (2)	Nei Tempi Previsti(3)	In Ritardo(4)	Inforte Ritardo
 case when
  ((sysdate - Nvl(t.datapianificata ,sysdate))* 1440) < 60 then '(1)In Gestione'
   when
  ((sysdate - Nvl(t.datapianificata ,sysdate))* 1440) < 120 then '(2)Nei Tempi Previsti'
    when
  ((sysdate - Nvl(t.datapianificata ,sysdate))* 1440) < 240 then '(3)	In Ritardo'
     when
  ((sysdate - Nvl(t.datapianificata ,sysdate))* 1440) > 240 then '(4)	In Forte Ritardo'
  end

 as Ritardo ,
---
       t.odl    as CodiceOdL,
       t.regrdl as CodRegRdL,

       e.oid as OidEdificio,
       cm.referentecofely as oidreferentecofely,
       t.categoria as oidcategoria,
       sm.oid as OIDSMISTAMENTO
  from RDL t,
       edificio e,
       indirizzo iz,
       comuni cu,
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
        (select li.oid,
        pi.descrizione as piano,
        li.descrizione as stanza
        from
        locali li,
        piani pi,
        categoriapiano cpi
        where cpi.oid=pi.categoriapiano
        and pi.oid=li.piani
        ) l,

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
               r.cognome || ' ' || r.nome || '(' || rt.anno || ')' as Team
          from risorseteam rt, risorse r
         where r.oid = rt.risorsacapo) rt
       ,

        ( select
         count(n.oid) as nrsolleciti,
         n.rdl from
         rdlnote n, rdl r
         where n.tiponota=0
         and r.oid=n.rdl
         group by  n.rdl) sr
--, (select o.oid,o.regrdl from odl o) o

 where e.oid = t.edificio
   and cm.oid = e.commessa
   and p.oid = t.priorita
   and tp.oid = t.tipointervento
   and i.oid = t.impianto
   and a.oid = t.apparato
   and c.oid = t.categoria
   and iz.oid=e.indirizzo
   and cu.oid=iz.comuni
   and arp.oid=cm.areadipolo
   and rt.oid(+) = t.risorsateam
   and sm.oid = t.statosmistamento
   and so.oid(+) = t.statooperativo
   and ri.oid = t.richiedente
   and ap.oid = a.stdapparato
   and prac.oid(+) = t.pcrappproblema
   and cau.oid(+) = t.pcrprobcausa
   and rim.oid(+) = t.pcrcausarimedio
   and sr.rdl(+) = t.oid
   and tir.oid=ri.tiporichiedente
   and l.oid(+)=t.locali
--and t.oid = 1554
--and o.regrdl(+)=t.regrdl
and amm.oid (+)=cm.clienticontatti
;
