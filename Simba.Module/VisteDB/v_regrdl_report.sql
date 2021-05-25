create or replace view v_regrdl_report as
select rg.oid as Codice,
       --t.oid as Codice,  no codice della rdl !!!!!!!!!!!!!!!!
   --    t.odl as CodiceOdL,
       e.descrizione as Edificio,
       cu.denominazioneitaliano as Comune,
       e.cod_descrizione as CodEdificio,
       arp.descrizione as AreadiPolo,
       cm.centrocosto as Area,
       i.descrizione as Impianto,
     --  a.descrizione as Apparato,
    --   ap.descrizione || '(' || ap.cod_uni || ')' as TipoApparato,

       case when sr.regrdl is not null
       then 'SI'
       else 'NO'
        end  as Solleciti,


       --prac.descrizione as problema,
       --cau.descrizione as causa,
       --rim.descrizione as rimedio,
       c.descrizione as CategoriaManutenzione,
       rg.descrizione,
     --  t.utenteinserimento as Utente,
     --  t.datacreazione as datacreazione,
       t.datarichiesta as datarichiesta,
    --   rg.dataupdate as dataaggiornamento,
       to_number(to_char(t.datarichiesta, 'IW')) as settimana,
       to_number(to_char(t.datarichiesta, 'MM')) as mese,
       to_number(to_char(t.datarichiesta, 'yyyy')) as anno,
       t.datapianificata as datapianificata,
   --    t.dataassegnazioneodl as dataassegnazione,
       t.datacompletamento,
       /*t.datariavvio
       t.datafermo,*/
       rt.team,
       rt.teammansione,
       sm.statosmistamento as ultimostatosmistamento,
       so.statooperativo   as ultimostatooperativo,

       t.notecompletamento  --,
    --   ri.nomecognome      as Richiedente,
   --    p.descrizione       as Priorita,
   --    tp.descrizione      as PrioritaTipoIntervento,

   --    e.oid as OidEdificio,
   --    cm.referentecofely as oidreferentecofely,
   --    t.categoria as oidcategoria

  from regrdl           rg,
       RDL              t,
       edificio         e,
       indirizzo        iz,
       comuni           cu,
       commesse         cm,
       areadipolo       arp,
       impianto         i,
       priorita         p,
       tipointervento   tp,
       Apparato         a,
       categoria        c,
       statosmistamento sm,
       statooperativo   so,
       apparatostd      ap,
       richiedente      ri,


       (select rt.oid,
               r.cognome || ' ' || r.nome || '(' || rt.anno || ')' as Team
               , m.descrizione teammansione
          from risorseteam rt, risorse r, MANSIONI M
         where r.oid = rt.risorsacapo
         and m.oid = r.mansione
         ) rt,
        (   select
         count(n.oid) as nrsolleciti,
         r.regrdl from
         rdlnote n,rdl r
         where n.tiponota=0
         and r.oid=n.rdl
         group by  r.regrdl) sr


 where rg.oid = t.regrdl
   and e.oid = t.edificio
   and cm.oid = e.commessa
   and arp.oid=cm.areadipolo
   and p.oid = t.priorita
   and tp.oid = t.tipointervento
   and i.oid = t.impianto
   and a.oid = t.apparato
   and c.oid = t.categoria
   and iz.oid=e.indirizzo
   and cu.oid=iz.comuni
   and rt.oid(+) = rg.risorsateam
   and sm.oid = rg.ultimostatosmistamento
   and so.oid(+) = rg.ultimostatooperativo
   and ri.oid = t.richiedente
   and ap.oid = a.stdapparato
   and sr.regrdl(+) = rg.oid



--and prac.oid(+)=t.pcrappproblema
--and cau.oid(+)=t.pcrprobcausa
--and rim.oid(+)=t.pcrcausarimedio
--and o.regrdl(+)=t.regrdl
;
