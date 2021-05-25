create or replace view v_rdl_list_stat as
select
v.AreadiPolo,v.Area, v.utenteinserimento, v.statosmistamento,
count(v.rdl) as conteggio
from
(select
a.cod_descrizione as AreadiPolo,
c.centrocosto as Area,
r.utenteinserimento,
t.statosmistamento,
r.oid as rdl
 from v_rdl_list t, edificio e, commesse c, areadipolo a, rdl r
where e.oid=t.OidEdificio
and e.commessa=c.oid
and a.oid=c.areadipolo
and r.oid=t.Codice
and t.datarichiesta>=to_date('07/11/2016','dd/mm/yyyy')
and c.centrocosto<>'PE') v
--order by a.cod_descrizione,c.centrocosto, t.datarichiesta
group by v.AreadiPolo,v.Area, v.utenteinserimento,v.statosmistamento
order by v.AreadiPolo,v.Area, v.utenteinserimento,v.statosmistamento
;
