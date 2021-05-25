create or replace view v_regrdl_list as
select  to_number( rg.oid ) as Codice,
       --t.oid as Codice,  no codice della rdl !!!!!!!!!!!!!!!!
       --- t.odl as CodiceOdL,
       --
       e.descrizione as Edificio, ----
       cu.denominazioneitaliano as Comune,
       e.cod_descrizione as CodEdificio,
       arp.descrizione as AreadiPolo,
       cm.centrocosto as Area,
       i.descrizione as Impianto, ---
       a.descrizione as Apparato, --
       ap.descrizione || '(' || ap.cod_uni || ')' as TipoApparato, --

/*       case
         when sr.regrdl is not null then
          'SI'
         else
          'NO'
       end as Solleciti,*/

       --prac.descrizione as problema,
       --cau.descrizione as causa,
       --rim.descrizione as rimedio,
       c.descrizione       as CategoriaManutenzione, --
      -- rg.descrizione, --
        case when length(rg.descrizione) > 500 then
        substr( rg.descrizione ,1,497) || '...'
        else
          rg.descrizione
          end
        as descrizione ,
      -- t.utenteinserimento as Utente, ---
       rg.utente           as Utente, ---
       -- t.datacreazione as datacreazione,
       rg.data_creazione_rdl as datacreazione, --
       -- t.datarichiesta as datarichiesta,          ---rg = registro  t=rdl
     --  (select min(rdl.datarichiesta) from rdl where rdl.regrdl = rg.oid) as datarichiesta,
       rg.data_creazione_rdl  as datarichiesta,
       rg.dataupdate as dataaggiornamento,
       to_number(to_char(rg.data_creazione_rdl, 'IW')) as settimana, --   to_number(to_char(t.datarichiesta, 'IW'))
       to_number(to_char(rg.data_creazione_rdl, 'MM')) as mese, --             to_number(to_char(t.datarichiesta, 'MM'))
       to_number(to_char(rg.data_creazione_rdl, 'yyyy')) as anno, ---      to_number(to_char(t.datarichiesta, 'yyyy'))
       -- t.datapianificata as datapianificata,
       (select min(rdl.datapianificata) from rdl where rdl.regrdl = rg.oid) as datapianificata,
       -- t.dataassegnazioneodl as dataassegnazione,
       rg.dataassegnazioneodl as dataassegnazione,
       rg.datacompletamento,
       /*t.datariavvio
       t.datafermo,*/
       rt.team,
       rt.teammansione,
       sm.statosmistamento as ultimostatosmistamento,
       so.statooperativo   as ultimostatooperativo,

       rg.notecompletamento as notecompletamento, ---       t.notecompletamento
       --   ri.nomecognome      as Richiedente,
       p.descrizione  as Priorita,
     --  tp.descrizione as PrioritaTipoIntervento,

       e.oid              as OidEdificio,
       cm.referentecofely as oidreferentecofely,
      --- t.categoria        as oidcategoria
       rg.categoria        as oidcategoria
  from regrdl           rg,
       RDL              t,
       edificio         e,
       indirizzo        iz,
       comuni           cu,
       commesse         cm,
       areadipolo       arp,
       impianto         i,
       priorita         p,
     --  tipointervento   tp,
       Apparato         a,
       categoria        c,
       statosmistamento sm,
       statooperativo   so,
       apparatostd      ap,
       --   richiedente      ri,

       (select rt.oid,
               r.cognome || ' ' || r.nome || '(' || rt.anno || ')' as Team,
               m.descrizione teammansione
          from risorseteam rt, risorse r, MANSIONI M
         where r.oid = rt.risorsacapo
           and m.oid = r.mansione) rt

/*       (select count(n.oid) as nrsolleciti, r.regrdl
          from rdlnote n, rdl r
         where n.tiponota = 0
           and r.oid = n.rdl
         group by r.regrdl) sr*/


 where rg.oid = t.regrdl
 --  and e.oid = t.edificio
 --  and cm.oid = e.commessa
   and arp.oid = cm.areadipolo
   and p.oid = rg.priorita
  -- and tp.oid = t.tipointervento
  -- and i.oid = t.impianto
  and e.commessa = cm.oid
  and i.edificio = e.oid
   and a.impianto = i.oid
   and a.oid = rg.apparato
   and c.oid = rg.categoria
   and iz.oid = e.indirizzo
   and cu.oid = iz.comuni
   and rt.oid(+) = rg.risorsateam
   and sm.oid = rg.ultimostatosmistamento
   and so.oid(+) = rg.ultimostatooperativo
      -- and ri.oid = t.richiedente
   and ap.oid = a.stdapparato
   and rg.data_creazione_rdl < sysdate +60
/*   and sr.regrdl(+) = rg.oid*/
  /* and rg.categoria = 4*/
/*   and rg.oid not in ( --4212
\* 4199,
11143,
4202,
4193,
4198,
4194,
4195,
4196,*\
--4210,
--4211,
--4212  ,
--11144,--
4201--,
--4197
   )*/
 /*  and rg.oid = 4201
and (rg.data_creazione_rdl < sysdate -119 and rg.data_creazione_rdl >  sysdate -120)*/
--and prac.oid(+)=t.pcrappproblema
--and cau.oid(+)=t.pcrprobcausa
--and rim.oid(+)=t.pcrcausarimedio
--and o.regrdl(+)=t.regrdl
;
