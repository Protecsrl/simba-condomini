--pubblicazioni da fare bluglio 2018
---   MostraDataOraFermo
update regrdl r
   set r.mostradatafermo =
    (select    nvl( ( select  case when  count( t.oid) > 0 then 1 else 0 end   
from autorizzazioniregrdl t
where   t.tipoautorizzazioni = 0
and t.regrdl = r.oid
group by t.regrdl
),0) val from dual
);
------ MostraDataOraRiavvio
  update regrdl r
   set r.mostradatariavvio =
    (select    nvl( ( select  case when  count( t.oid) > 0 then 1 else 0 end   
from autorizzazioniregrdl t
where   t.tipoautorizzazioni = 1
and t.regrdl = r.oid
group by t.regrdl
),0) val from dual
);
-------------  MostraDataOraSopralluogo
update regrdl r
   set r.mostradatasopralluogo =
    (select    nvl( ( select  case when  count( t.oid) > 0 then 1 else 0 end   
from autorizzazioniregrdl t
where   t.tipoautorizzazioni = 2
and t.regrdl = r.oid
group by t.regrdl
),0) val from dual
);       
 -------------  MostraDataOraAzioniTampone
update regrdl r
   set r.mostradatasicurezza =
    (select    nvl( ( select  case when  count( t.oid) > 0 then 1 else 0 end   
from autorizzazioniregrdl t
where   t.tipoautorizzazioni = 3
and t.regrdl = r.oid
group by t.regrdl
),0) val from dual
);     
  -------------  MostraDataOraInizioLavori
update regrdl r
   set r.mostradatainiziolavori =
    (select    nvl( ( select  case when  count( t.oid) > 0 then 1 else 0 end   
from autorizzazioniregrdl t
where   t.tipoautorizzazioni = 4
and t.regrdl = r.oid
group by t.regrdl
),0) val from dual
);     
   -------------  MostraDataOraCompletamento
update regrdl r
   set r.mostradatainiziolavori =
    (select    nvl( ( select  case when  count( t.oid) > 0 then 1 else 0 end   
from autorizzazioniregrdl t
where   t.tipoautorizzazioni = 5
and t.regrdl = r.oid
group by t.regrdl
),0) val from dual
); 
     -------------  MostraElencoCauseRimedi
update regrdl r
   set r.mostradatainiziolavori =
    (select    nvl( ( select  case when  count( t.oid) > 0 then 1 else 0 end   
from autorizzazioniregrdl t
where   t.tipoautorizzazioni = 6
and t.regrdl = r.oid
group by t.regrdl
),0) val from dual
); 
 
 --- aggiurna apparato    con dato di default   non definito
 update  impianto i set i.apparato_default = 
(
select max(a.oid) from apparato a where a.impianto = i.oid and upper( a.descrizione) like '%NON DEFINITO%'

)
where i.apparato_default is null

