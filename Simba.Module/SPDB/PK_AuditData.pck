CREATE OR REPLACE PACKAGE PK_AuditData is

  TYPE T_CURSOR IS REF CURSOR;
  /*    OID TypeName                TargetType  TargetKey Oid OperationType UserName  ModifiedOn  DESC_VARIAZIONE OldObject NewObject OldValue  NewValue  PropertyName
  15  19  CAMS.Module.DBPlant.Apparato  19  [Int32]'22557'  7ecf8700-a072-4f13-8891-b807566823d5  AddedToCollection Web user (Sam)  19/03/2018 18:26:32 AddedToCollection; App Illuminazione (CMA_S00647_PL00647) Strada dei Colli; RdLs; Prova19/03/2018(59942); N/A 85f8adc2-9aae-4e6a-9d2f-93a84fba7602    Prova19/03/2018(59942)  N/A RdLs
  16  25  CAMS.Module.DBTask.RdL        25  [Int32]'59942'  3f511ec7-a958-4118-9f48-e41cc98a1d3f  InitialValueAssigned  Web user (Sam)  19/03/2018 18:26:32 InitialValueAssigned; Prova19/03/2018(59942); Edificio; N/A; Comune di Castel Madama    dbd77f60-aca8-4ea2-953e-78cb810fa71d  N/A Comune di Castel Madama Edificio
  */
  cursor cGetAudit(pOidObj number, TypeObj varchar2, pdatalimite date) is
    select t."OID",
           t."TypeName",
           a."Oid",
           a."OperationType",
           a."UserName",
           a."ModifiedOn",
           to_char(a."Description") as DESC_VARIAZIONE,
           a."OldObject",
           a."NewObject",
           a."OldValue",
           a."NewValue",
           a."PropertyName"
      from "AuditDataItemPersistent" a,
           "XPWeakReference"         r,
           "XPObjectType"            t
     where r."TargetType" = t."OID"
       and a."AuditedObject" = r."Oid"
       and t."OID" = pOidObj --25 = RDL;
          --   and t."TargetType" = 25 -----25 = RDL;
       and r."TargetKey" = TypeObj
       and a."ModifiedOn" > pdatalimite
    --and to_char(a."Description") like '%26043%'
     order by a."ModifiedOn" desc;

  type Tab_cGetAudit is table of cGetAudit%rowtype;

  procedure GetAuditData(iOidObj     in varchar,
                         iTypeObj    in varchar,
                         iDataLimite in varchar,
                         iusername   in varchar2,
                         oMessaggio   in out varchar,
                         IO_CURSOR   IN OUT T_CURSOR);

  procedure  GetAuditData_REGRDL(iOidObj     in varchar,
                         --iDataLimite in varchar,
                         --iusername   in varchar2,
                         oMessaggio   in out varchar,
                         IO_CURSOR   IN OUT T_CURSOR) ;
  procedure LINSVARIAZAUDITREG;

  procedure  GetAuditData_REGRDL_SM(iOidObj in varchar,
                         --iDataLimite in varchar,
                         --iusername   in varchar2,
                         oMessaggio   in out varchar
                         --,IO_CURSOR   IN OUT T_CURSOR
                         );

  procedure  GetAuditData_REGRDL_SO(iOidObj     in varchar,
                         --iDataLimite in varchar,
                         --iusername   in varchar2,
                         oMessaggio   in out varchar
                         --,IO_CURSOR   IN OUT T_CURSOR
                         );
  procedure aggdeltasmistamento(icodregrdl in number);
  procedure aggdeltaoperativo(icodregrdl in number);
end PK_AuditData;
/
create or replace package body PK_AuditData is

  -- kpi
  /*  PROCEDURE GetRdLSeg_send_Mail(iUser       in varchar,
  isearchtext in varchar,
  oErrorMsg   in out varchar,
  IO_CURSOR   IN OUT T_CURSOR) is*/

 procedure GetAuditData(iOidObj     in varchar,                         
                         iTypeObj    in varchar,  
                         iDataLimite in varchar,
                         iusername   in varchar2,
                         oMessaggio   in out varchar,
                         IO_CURSOR   IN OUT T_CURSOR) is
    indice number := 0;
  
    Messaggio varchar2(4000) := null;
  
    TargetKey   varchar2(4000) := null;
    TypeObj     number;
    v_conteggio  number:=0;  
    pdatalimite date;
    V_CURSOR    T_CURSOR;
  begin
  
    indice      := 0;
    TypeObj     := 25;
    pdatalimite := sysdate - 1;
    TargetKey   := '';
    
    insert into tbl_sql
    (tbl_sql.s_sql, tbl_sql.data)
    values
    (
    'SP:GetAuditData '||chr(10)||   
    'iOidObj '  ||iOidObj||chr(10)||                     
    'iTypeObj '  ||iTypeObj||chr(10)||    
    'iDataLimite '||iDataLimite||chr(10)||  
    'iusername ' ||   iusername   
    ,
    sysdate    
    );
    commit;
    
    select '''|| iOid||''' into TargetKey from dual;
    /*  
    ID = Newid,
    RdLCodice = dr.GetString(dr.GetOrdinal("RDL_CODICE")),
    Descrizione = dr.GetString(dr.GetOrdinal("DESCRIZIONE")),
    DataOra = dr.GetDateTime(dr.GetOrdinal("DATAORA")),
    UtenteSO = dr.GetString(dr.GetOrdinal("UTENTESO")),
    RisorseTeam = dr.GetString(dr.GetOrdinal("RISORSETEAM")),
    OldRisorseTeam = dr.GetString(dr.GetOrdinal("RISORSETEAM_OLD")),
    SSmistamentoDescrizione = dr.GetString(dr.GetOrdinal("SSMISTAMENTODESCRIZIONE")),
    OldSSmistamentoDescrizione = dr.GetString(dr.GetOrdinal("SSMISTAMENTODESCRIZIONE_OLD")),
    SOperativoDescrizione = dr.GetString(dr.GetOrdinal("SOPERATIVODESCRIZIONE")),
    OldSOperativoDescrizione = dr.GetString(dr.GetOrdinal("SOPERATIVODESCRIZIONE_OLD")),
    DataPianificata = dr.GetDateTime(dr.GetOrdinal("DATAPIANIFICATA")),
    OldDataPianificata = dr.GetDateTime(dr.GetOrdinal("DATAPIANIFICATA_OLD"))
    */
    
        select 
        count(*) into v_conteggio
        from "AuditDataItemPersistent" a,
             "XPWeakReference"         r,
             "XPObjectType"            t
       where r."TargetType" = t."OID"
         and a."AuditedObject" = r."Oid"
         and t."OID" = 25 -- TypeObj --25 = RDL;
            --   and t."TargetType" = 25 -----25 = RDL;
         and r."TargetKey" = '[Int32]''' || iOidObj || '''' -- '[Int32]'''||'48137'||'''' --    and r."TargetKey" like '%48137%' -- TargetKey 
         and a."ModifiedOn" > sysdate - 1000
         and a."PropertyName" 
         not in 
         ('Richiedente','Edificio','Soddisfazione','Impianto',
         'DataSopralluogo','TipoIntervento','Priorita',
         'Apparato','Categoria',
         'DataAzioniTampone','TipologiaSpedizione',
         'OdL','DataAggiornamento',
         'DataAssegnazioneOdl','Problema',
         'ProblemaCausa','CausaRimedio',
         'old_SSmistamento_Oid',
         'StatoAutorizzativo','DataPianificataEnd',
         'DataPrevistoArrivo','DataInizioLavori'
         )
      --and to_char(a."Description") like '%26043%'
       ;
    
    
    OPEN V_CURSOR FOR
       
        select --'[Int32]''' || '48137' || '''' as mio,
            /* replace(r."TargetKey"*/
             
            --t."OID",
            --t."TypeName",
            replace(REPLACE(r."TargetKey", ''''),'[Int32]') as RDL_CODICE,
             --r."TargetKey" as RDL_CODICE,
            a."Oid",
            a."OperationType" as AZIONEPROPERTYNAME,
            a."UserName" as UTENTE,
            a."ModifiedOn" as DATAORA,
            Nvl( a."PropertyName",'nd') || '-' || to_char(a."Description") as DESCRIZIONE,
            Nvl( a."NewValue",'nd') as  NUOVOVALORE,
            Nvl( a."OldValue",'nd')  as CAMBIOVALORE,
            Nvl( a."PropertyName",'nd')  as PROPERTYNAME
            /*,
          
          case when a."PropertyName"= 'RisorseTeam' 
          then 
          Nvl(a."NewValue",'nd') 
         else
          a."OldValue"
          end   
           as RISORSETEAM_DESC,
         
         case when a."PropertyName"= 'RisorseTeam' 
           then 
         Nvl( a."OldValue",'nd') 
         else
           a."OldValue"
         end   
         as RISORSETEAM_DESC_OLD,
           
          case when a."PropertyName"='UltimoStatoSmistamento' 
          then   
          Nvl(a."OldValue",'nd') 
          else
           a."OldValue"
          end   
          as SSMISTAMENTO_DESC,
          case when a."PropertyName"='UltimoStatoSmistamento' 
          then  
            Nvl( a."NewValue",'nd') 
           else
            a."OldValue"
          end                 
            as SSMISTAMENTO_DESC_OLD,
           case when a."PropertyName"='UltimoStatoOperativo' 
          then   
           Nvl(a."OldValue",'nd') 
           else
           a."OldValue"
           end 
           as SOPERATIVO_DESC,
           case when a."PropertyName"='UltimoStatoOperativo' 
          then   
           Nvl(a."NewValue",'nd') 
           else
            a."OldValue"
           end 
           as SOPERATIVO_DESC_OLD,
          
         case when a."PropertyName"='DataPianificata' 
          then   
           Nvl(a."NewValue",'nd') 
           else
             a."OldValue"
           end  
            as DATAPIANIFICATA,
          
           case when a."PropertyName"='DataPianificata' 
          then  
             Nvl(a."OldValue",'nd')                       
          else
            a."OldValue"
           end 
           as DATAPIANIFICATA_OLD,
           */
        
        from "AuditDataItemPersistent" a,
             "XPWeakReference"         r,
             "XPObjectType"            t
       where r."TargetType" = t."OID"
         and a."AuditedObject" = r."Oid"
         and t."OID" = 25 -- TypeObj --25 = RDL;
            --   and t."TargetType" = 25 -----25 = RDL;
         and r."TargetKey" = '[Int32]''' || iOidObj || '''' -- '[Int32]'''||'48137'||'''' --    and r."TargetKey" like '%48137%' -- TargetKey 
         and a."ModifiedOn" > sysdate - 1000
         and a."PropertyName" 
         not in 
         ('Richiedente','Edificio','Soddisfazione','Impianto',
         'DataSopralluogo','TipoIntervento','Priorita',
         'Apparato','Categoria',
         'DataAzioniTampone','TipologiaSpedizione',
         'OdL','DataAggiornamento',
         'DataAssegnazioneOdl','Problema',
         'ProblemaCausa','CausaRimedio',
         'old_SSmistamento_Oid',
         'StatoAutorizzativo','DataPianificataEnd',
         'DataPrevistoArrivo','DataInizioLavori'
         )
      --and to_char(a."Description") like '%26043%'
       order by a."ModifiedOn"/*,a."Oid" */ desc;
    IO_CURSOR := V_CURSOR;
  
    --  EXCEPTION
    --    WHEN OTHERS THEN
    -- ROLLBACK;
    -- DATAORA_successiva := cSSmistamento.dataora;
  
  end GetAuditData;
  


 procedure  GetAuditData_REGRDL(iOidObj     in varchar,
                         --iDataLimite in varchar,
                         --iusername   in varchar2,
                         oMessaggio   in out varchar,
                         IO_CURSOR   IN OUT T_CURSOR) is
    indice number := 0;

    Messaggio varchar2(4000) := null;

    TargetKey   varchar2(4000) := null;
    TypeObj     number;
    pdatalimite date;
    V_CURSOR    T_CURSOR;
  begin

    indice      := 0;
    TypeObj     := 25;
    pdatalimite := sysdate - 1;
    TargetKey   := '';
    select '''|| iOid||''' into TargetKey from dual;
    /*
    ID = Newid,
    RdLCodice = dr.GetString(dr.GetOrdinal("RDL_CODICE")),
    Descrizione = dr.GetString(dr.GetOrdinal("DESCRIZIONE")),
    DataOra = dr.GetDateTime(dr.GetOrdinal("DATAORA")),
    UtenteSO = dr.GetString(dr.GetOrdinal("UTENTESO")),
    RisorseTeam = dr.GetString(dr.GetOrdinal("RISORSETEAM")),
    OldRisorseTeam = dr.GetString(dr.GetOrdinal("RISORSETEAM_OLD")),
    SSmistamentoDescrizione = dr.GetString(dr.GetOrdinal("SSMISTAMENTODESCRIZIONE")),
    OldSSmistamentoDescrizione = dr.GetString(dr.GetOrdinal("SSMISTAMENTODESCRIZIONE_OLD")),
    SOperativoDescrizione = dr.GetString(dr.GetOrdinal("SOPERATIVODESCRIZIONE")),
    OldSOperativoDescrizione = dr.GetString(dr.GetOrdinal("SOPERATIVODESCRIZIONE_OLD")),
    DataPianificata = dr.GetDateTime(dr.GetOrdinal("DATAPIANIFICATA")),
    OldDataPianificata = dr.GetDateTime(dr.GetOrdinal("DATAPIANIFICATA_OLD"))
    */
    OPEN V_CURSOR FOR
        select --'[Int32]''' || '48137' || '''' as mio,
            /* replace(r."TargetKey"*/

             --t."OID",
             --t."TypeName",
             replace(REPLACE(r."TargetKey", ''''),'[Int32]') as RDL_CODICE,
             rg.oid as REGRDL,
             --r."TargetKey" as RDL_CODICE,
             --a."Oid",
             a."OperationType",
             a."UserName" as UTENTE,
             a."ModifiedOn" as DATAORA,
           Nvl( a."PropertyName",'nd') || '-' || to_char(a."Description") as DESCRIZIONE,
        /*
           Nvl( a."OldObject",'nd') as RISORSETEAM_DESC_OLD,
           Nvl( a."NewObject",'nd') as RISORSETEAM_DESC,
           Nvl( a."OldValue",'nd') as SSMISTAMENTO_DESC,
           Nvl( a."NewValue",'nd') as SSMISTAMENTO_DESC_OLD,
           Nvl( a."OldValue",'nd') as SOPERATIVO_DESC,
           Nvl( a."NewValue",'nd') as SOPERATIVO_DESC_OLD,

             a."ModifiedOn" as DATAPIANIFICATA,
             a."ModifiedOn" as DATAPIANIFICATA_OLD,*/
         case when a."PropertyName"= 'RisorseTeam'
          then
         Nvl( a."OldValue",'nd')
         else
          null
         end
         as RISORSETEAM_DESC_OLD,

         case when a."PropertyName"= 'RisorseTeam'
          then
          Nvl(  a."NewValue",'nd')
         else
          null
          end
           as RISORSETEAM_DESC,
          case when a."PropertyName"='UltimoStatoSmistamento'
          then
          Nvl(   a."OldValue",'nd')
          else
         null
          end
          as SSMISTAMENTO_DESC,
          case when a."PropertyName"='UltimoStatoSmistamento'
          then
            Nvl( a."NewValue",'nd')
           else
          null
          end
            as SSMISTAMENTO_DESC_OLD,
           case when a."PropertyName"='UltimoStatoOperativo'
          then
           Nvl(  a."OldValue",'nd')
           else
             null
           end
           as SOPERATIVO_DESC,
           case when a."PropertyName"='UltimoStatoOperativo'
          then
           Nvl(  a."NewValue",'nd')
           else
             null
           end
           as SOPERATIVO_DESC_OLD,
            case when a."PropertyName"='DataPianificata'
          then
            a."NewValue"
           else
             null
           end
            as DATAPIANIFICATA,
          case when a."PropertyName"='DataPianificata'
          then
              a."OldValue"
          else
             null
           end
           as DATAPIANIFICATA_OLD,
          Nvl(   a."PropertyName",'nd') as PropertyName
        from "AuditDataItemPersistent" a,
             "XPWeakReference"         r,
             "XPObjectType"            t,
             rdl rr,
             regrdl rg
       where r."TargetType" = t."OID"
         and a."AuditedObject" = r."Oid"
         and t."OID" = 25 -- TypeObj --25 = RDL;
            --   and t."TargetType" = 25 -----25 = RDL;
         and r."TargetKey" = '[Int32]''' || iOidObj || '''' -- '[Int32]'''||'48137'||'''' --    and r."TargetKey" like '%48137%' -- TargetKey
         and a."ModifiedOn" > sysdate - 1000
         and replace(REPLACE(r."TargetKey", ''''),'[Int32]')=rr.oid
         and rr.regrdl=rg.oid
         and a."PropertyName"  not in
         ('Richiedente','Edificio','Soddisfazione','Impianto',
         'DataSopralluogo','TipoIntervento','Priorita',
         'Apparato','Categoria',
         'DataAzioniTampone','TipologiaSpedizione',
         'OdL','DataAggiornamento',
         'DataAssegnazioneOdl','Problema',
         'ProblemaCausa','CausaRimedio',
         'old_SSmistamento_Oid',
         'StatoAutorizzativo','DataPianificataEnd',
         'DataPrevistoArrivo','DataInizioLavori'
         )
       order by a."ModifiedOn"/*,a."Oid" */ asc;
    IO_CURSOR := V_CURSOR;

    --  EXCEPTION
    --    WHEN OTHERS THEN
    -- ROLLBACK;
    -- DATAORA_successiva := cSSmistamento.dataora;

  end GetAuditData_REGRDL;

  procedure LINSVARIAZAUDITREG
    is
    v_message varchar2(50):='';
    begin
    for nt in
    (
 select
rg.oid as regrdl,
x.oid_rdl
from
 (select
      distinct
      to_number(replace(REPLACE(r."TargetKey", ''''),'[Int32]')) as oid_rdl
      --,to_char(a."ModifiedOn",'dd/mm/yyyy')
      from "AuditDataItemPersistent" a,
      "XPWeakReference"         r,
      "XPObjectType"            t
      where r."TargetType" = t."OID"
      and a."AuditedObject" = r."Oid"
      and t."OID" = 25
      and a."ModifiedOn">sysdate -3
      order by  to_number(replace(REPLACE(r."TargetKey", ''''),'[Int32]'))
      ) x,
     rdl r,
     regrdl rg
 where
 r.oid=x.oid_rdl
 and r.regrdl=rg.oid
    )
    loop
    GetAuditData_REGRDL_SM(nt.oid_rdl,v_message );
    GetAuditData_REGRDL_SO(nt.oid_rdl,v_message );
    aggdeltasmistamento(nt.regrdl);
    aggdeltaoperativo(nt.regrdl);
    end loop;
    commit;
  end LINSVARIAZAUDITREG;

  procedure  GetAuditData_REGRDL_SM(iOidObj in varchar,
                         --iDataLimite in varchar,
                         --iusername   in varchar2,
                         oMessaggio   in out varchar
                         --,IO_CURSOR   IN OUT T_CURSOR
                         ) is
    indice number := 0;

    Messaggio varchar2(4000) := null;

    TargetKey   varchar2(4000) := null;
    TypeObj     number;
    pdatalimite date;
    v_conto_ins number:=0;
    --V_CURSOR    T_CURSOR;
  begin

    indice      := 0;
    TypeObj     := 25;
    pdatalimite := sysdate - 1;
    TargetKey   := '';
    select '''|| iOid||''' into TargetKey from dual;


  for nn in
  (
  select
  x.oidrisorseteam,
  y.REGRDL,
  y.utente,
  y.DATAORA,
  y.OIDSTATOSMISTAMENTO,
  y.DESCRIZIONE,
  (select s.oid  from statosmistamento s where s.statosmistamento=y.SSMISTAMENTO_DESC_OLD)
  as  OIDSTATOSMISTAMENTOOLD
   from

(select
--replace(REPLACE(r."TargetKey", ''''),'[Int32]') as RDL_CODICE,
rg.oid|| a."UserName"||to_char(a."ModifiedOn",'dd/mm/yyyy hh24:mi:ss')  as codex,
rg.oid as REGRDL,
--a."OperationType",
a."UserName" as UTENTE,
a."ModifiedOn" as DATAORA,

sm.oid as oidstatosmistamento,
case when a."PropertyName"='UltimoStatoSmistamento'
then
Nvl(   a."NewValue",'nd')
else
null
end
as SSMISTAMENTO_DESC,

case when a."PropertyName"='UltimoStatoSmistamento'
then
Nvl( a."OldValue",'nd')
else
null
end
as SSMISTAMENTO_DESC_OLD,

case when  a."OldValue"='N/A'
  then
    a."NewValue"
  else
    'Da ' ||a."OldValue"||' A ' || a."NewValue"
  end as descrizione,
Nvl(   a."PropertyName",'nd') as PropertyName
from "AuditDataItemPersistent" a,
"XPWeakReference"         r,
"XPObjectType"            t,
rdl rr,
regrdl rg,
statosmistamento sm


where r."TargetType" = t."OID"
and a."AuditedObject" = r."Oid"
and t."OID" = 25 -- TypeObj --25 = RDL;
--   and t."TargetType" = 25 -----25 = RDL;
and r."TargetKey" = '[Int32]''' || iOidObj || '''' --iOidObj '[Int32]'''||'48137'||'''' --    and r."TargetKey" like '%48137%' -- TargetKey
and a."ModifiedOn" > sysdate - 1000
and replace(REPLACE(r."TargetKey", ''''),'[Int32]')=rr.oid
and rr.regrdl=rg.oid
and a."PropertyName"  in
('UltimoStatoSmistamento'
)
and sm.statosmistamento= a."NewValue"
) y,

(select
rg.oid|| a."UserName"||to_char(a."ModifiedOn",'dd/mm/yyyy hh24:mi:ss')  as codex,
rg.oid as REGRDL,
a."ModifiedOn" as DATAORA,
ris.oid as oidrisorseteam,
a."UserName" as UTENTESO,
case when a."PropertyName"= 'RisorseTeam'
then
Nvl(  a."NewValue",'nd')
else
null
end
as RISORSETEAM_DESC,
case when a."PropertyName"= 'RisorseTeam'
then
Nvl( a."OldValue",'nd')
else
null
end
as RISORSETEAM_DESC_OLD
from "AuditDataItemPersistent" a,
"XPWeakReference"         r,
"XPObjectType"            t,
rdl rr,
regrdl rg,
(select
r.nome||' '||r.cognome||'('||r.cognome||' - '||rs.anno||')' as strrs,
rs.oid
from risorseteam rs, risorse r
where r.oid=rs.risorsacapo) ris

where r."TargetType" = t."OID"
and a."AuditedObject" = r."Oid"
and t."OID" = 25 -- TypeObj --25 = RDL;
--   and t."TargetType" = 25 -----25 = RDL;
and r."TargetKey" = '[Int32]''' || iOidObj || '''' -- iOidObj '[Int32]'''||'48137'||'''' --    and r."TargetKey" like '%48137%' -- TargetKey
and a."ModifiedOn" > sysdate - 1000
and replace(REPLACE(r."TargetKey", ''''),'[Int32]')=rr.oid
and rr.regrdl=rg.oid
and a."PropertyName"  in
('RisorseTeam')
and ris.strrs= a."NewValue" ) x
where x.codex(+)=y.codex
order by y.dataora asc
 )
loop
  select count(*)
  into
  v_conto_ins
  from
  regsmistamentodett
  where
  regrdl||
  statosmistamento||
  to_char(dataora,'dd/mm/yyyy hh24:mi ss')
  =
  nn.regrdl||
  nn.oidstatosmistamento||
  to_char(nn.dataora,'dd/mm/yyyy hh24:mi ss')
  ;
  if v_conto_ins =0
  then
  insert into regsmistamentodett
    (oid,
    regrdl, statosmistamento,
    dataora,descrizione,
    deltatempo, risorsateam, utente,
    statosmistamento_old)
  values
    ("sq_REGSMISTAMENTODETT".nextval,
    nn.regrdl,nn.oidstatosmistamento,
    nn.dataora,nn.descrizione,
    0, nn.oidrisorseteam, nn.utente,
    nn.oidstatosmistamentoold);

  commit;
  end if;
/*OID  N  NUMBER
REGRDL  N  NUMBER(38)
STATOSMISTAMENTO  N  NUMBER(38)
DATAORA  N  DATE
ICONA  N  NUMBER(3)
OptimisticLockField  N  NUMBER(38)
GCRecord  N  NUMBER(38)
DESCRIZIONE  N  VARCHAR2(100)
DELTATEMPO  N  NUMBER(38)
RISORSATEAM  N  NUMBER(38)
UTENTE  N  VARCHAR2(4000)
STATOSMISTAMENTO_OLD  N  INTEGER
STATOOPERATIVO  N  INTEGER
STATOOPERATIVO_OLD  N  INTEGER
DATAORA_OLD  N  DATE
DESCRIZIONETRISORSA  N  VARCHAR2(500)
DESCRIZIONESOPERATIVO  N  VARCHAR2(1000)
*/

end loop;


  /*OPEN V_CURSOR FOR

  select
  x.oidrisorseteam,
  y.REGRDL,
  y.utente,
  y.DATAORA,
  y.OIDSTATOSMISTAMENTO,
  y.DESCRIZIONE
   from

(select
--replace(REPLACE(r."TargetKey", ''''),'[Int32]') as RDL_CODICE,
rg.oid|| a."UserName"||to_char(a."ModifiedOn",'dd/mm/yyyy hh24:mi:ss')  as codex,
rg.oid as REGRDL,
--a."OperationType",
a."UserName" as UTENTE,
a."ModifiedOn" as DATAORA,

sm.oid as oidstatosmistamento,
case when a."PropertyName"='UltimoStatoSmistamento'
then
Nvl(   a."NewValue",'nd')
else
null
end
as SSMISTAMENTO_DESC,

case when a."PropertyName"='UltimoStatoSmistamento'
then
Nvl( a."OldValue",'nd')
else
null
end
as SSMISTAMENTO_DESC_OLD,

case when  a."OldValue"='N/A'
  then
    a."NewValue"
  else
    'Da ' ||a."OldValue"||' A ' || a."NewValue"
  end as descrizione,
Nvl(   a."PropertyName",'nd') as PropertyName
from "AuditDataItemPersistent" a,
"XPWeakReference"         r,
"XPObjectType"            t,
rdl rr,
regrdl rg,
statosmistamento sm


where r."TargetType" = t."OID"
and a."AuditedObject" = r."Oid"
and t."OID" = 25 -- TypeObj --25 = RDL;
--   and t."TargetType" = 25 -----25 = RDL;
and r."TargetKey" = '[Int32]''' || iOidObj || '''' --iOidObj '[Int32]'''||'48137'||'''' --    and r."TargetKey" like '%48137%' -- TargetKey
and a."ModifiedOn" > sysdate - 1000
and replace(REPLACE(r."TargetKey", ''''),'[Int32]')=rr.oid
and rr.regrdl=rg.oid
and a."PropertyName"  in
('UltimoStatoSmistamento'
)
and sm.statosmistamento= a."NewValue"
) y,

(select
rg.oid|| a."UserName"||to_char(a."ModifiedOn",'dd/mm/yyyy hh24:mi:ss')  as codex,
rg.oid as REGRDL,
a."ModifiedOn" as DATAORA,
ris.oid as oidrisorseteam,
a."UserName" as UTENTESO,
case when a."PropertyName"= 'RisorseTeam'
then
Nvl(  a."NewValue",'nd')
else
null
end
as RISORSETEAM_DESC,
case when a."PropertyName"= 'RisorseTeam'
then
Nvl( a."OldValue",'nd')
else
null
end
as RISORSETEAM_DESC_OLD
from "AuditDataItemPersistent" a,
"XPWeakReference"         r,
"XPObjectType"            t,
rdl rr,
regrdl rg,
(select
r.nome||' '||r.cognome||'('||r.cognome||' - '||rs.anno||')' as strrs,
rs.oid
from risorseteam rs, risorse r
where r.oid=rs.risorsacapo) ris

where r."TargetType" = t."OID"
and a."AuditedObject" = r."Oid"
and t."OID" = 25 -- TypeObj --25 = RDL;
--   and t."TargetType" = 25 -----25 = RDL;
and r."TargetKey" = '[Int32]''' || iOidObj || '''' -- iOidObj '[Int32]'''||'48137'||'''' --    and r."TargetKey" like '%48137%' -- TargetKey
and a."ModifiedOn" > sysdate - 1000
and replace(REPLACE(r."TargetKey", ''''),'[Int32]')=rr.oid
and rr.regrdl=rg.oid
and a."PropertyName"  in
('RisorseTeam')
and ris.strrs= a."NewValue" ) x
where x.codex(+)=y.codex
order by y.dataora asc
;*/

--    IO_CURSOR := V_CURSOR;

    --  EXCEPTION
    --    WHEN OTHERS THEN
    -- ROLLBACK;
    -- DATAORA_successiva := cSSmistamento.dataora;

  end GetAuditData_REGRDL_SM;

  procedure  GetAuditData_REGRDL_SO(iOidObj     in varchar,
                         --iDataLimite in varchar,
                         --iusername   in varchar2,
                         oMessaggio   in out varchar/*,
                         IO_CURSOR   IN OUT T_CURSOR*/

                         ) is
    indice number := 0;

    Messaggio varchar2(4000) := null;

    TargetKey   varchar2(4000) := null;
    TypeObj     number;
    pdatalimite date;
    --V_CURSOR    T_CURSOR;
    v_conto_ins number:=0;
  begin

    indice      := 0;
    TypeObj     := 25;
    pdatalimite := sysdate - 1;
    TargetKey   := '';
    select '''|| iOid||''' into TargetKey from dual;

/*  OIDRISORSETEAM
CODEX
REGRDL
UTENTE
DATAORA
OIDSTATOOPERATIVO
SOPERATIVO_DESC
SOPERATIVO_DESC_DESC_OLD
DESCRIZIONE
PROPERTYNAME
*/
 for nv in
   (
  select
  x.oidrisorseteam,
  y.REGRDL,
  y.UTENTE,
  y.DATAORA,
  y.OIDSTATOOPERATIVO,
  y.DESCRIZIONE
   from

(select
--replace(REPLACE(r."TargetKey", ''''),'[Int32]') as RDL_CODICE,
rg.oid|| a."UserName"||to_char(a."ModifiedOn",'dd/mm/yyyy hh24:mi:ss')  as codex,
rg.oid as REGRDL,
--a."OperationType",
a."UserName" as UTENTE,
a."ModifiedOn" as DATAORA,

so.oid as oidstatooperativo,
case when a."PropertyName"='UltimoStatoOperativo'
then
Nvl(   a."NewValue",'nd')
else
null
end
as SOPERATIVO_DESC,

case when a."PropertyName"='UltimoStatoOperativo'
then
Nvl( a."OldValue",'nd')
else
null
end
as SOPERATIVO_DESC_DESC_OLD,

case when  a."OldValue"='N/A'
  then
    a."NewValue"
  else
    'Da ' ||a."OldValue"||' A ' || a."NewValue"
  end as descrizione,

Nvl(   a."PropertyName",'nd') as PropertyName
from "AuditDataItemPersistent" a,
"XPWeakReference"         r,
"XPObjectType"            t,
rdl rr,
regrdl rg,
statooperativo so

where r."TargetType" = t."OID"
and a."AuditedObject" = r."Oid"
and t."OID" = 25 -- TypeObj --25 = RDL;
--   and t."TargetType" = 25 -----25 = RDL;
and r."TargetKey" = '[Int32]''' || iOidObj || '''' --iOidObj '[Int32]'''||'48137'||'''' --    and r."TargetKey" like '%48137%' -- TargetKey
and a."ModifiedOn" > sysdate - 1000
and replace(REPLACE(r."TargetKey", ''''),'[Int32]')=rr.oid
and rr.regrdl=rg.oid
and a."PropertyName"  in
('UltimoStatoOperativo'
)
and so.codstato= a."NewValue"
) y,

(select
rg.oid|| a."UserName"||to_char(a."ModifiedOn",'dd/mm/yyyy hh24:mi:ss')  as codex,
rg.oid as REGRDL,
a."ModifiedOn" as DATAORA,
ris.oid as oidrisorseteam,
a."UserName" as UTENTESO,
case when a."PropertyName"= 'RisorseTeam'
then
Nvl(  a."NewValue",'nd')
else
null
end
as RISORSETEAM_DESC,
case when a."PropertyName"= 'RisorseTeam'
then
Nvl( a."OldValue",'nd')
else
null
end
as RISORSETEAM_DESC_OLD
from "AuditDataItemPersistent" a,
"XPWeakReference"         r,
"XPObjectType"            t,
rdl rr,
regrdl rg,
(select
r.nome||' '||r.cognome||'('||r.cognome||' - '||rs.anno||')' as strrs,
rs.oid
from risorseteam rs, risorse r
where r.oid=rs.risorsacapo) ris

where r."TargetType" = t."OID"
and a."AuditedObject" = r."Oid"
and t."OID" = 25 -- TypeObj --25 = RDL;
--   and t."TargetType" = 25 -----25 = RDL;
and r."TargetKey" = '[Int32]''' || iOidObj || '''' -- iOidObj '[Int32]'''||'48137'||'''' --    and r."TargetKey" like '%48137%' -- TargetKey
and a."ModifiedOn" > sysdate - 1000
and replace(REPLACE(r."TargetKey", ''''),'[Int32]')=rr.oid
and rr.regrdl=rg.oid
and a."PropertyName"  in
('RisorseTeam')
and ris.strrs= a."NewValue" ) x
where x.codex(+)=y.codex
order by y.dataora asc

   )
loop

 select count(*)
  into
  v_conto_ins
  from
  regoperativodettaglio
  where
  regrdl||
  statooperativo||
  to_char(dataora,'dd/mm/yyyy hh24:mi ss')
  =
  nv.regrdl||
  nv.oidstatooperativo||
  to_char(nv.dataora,'dd/mm/yyyy hh24:mi ss')
  ;
 if v_conto_ins=0
 then
 insert into regoperativodettaglio
   (oid,
   regrdl,
   dataora,
   descrizione,
   deltatempo,
   statooperativo,
   utente)
 values
   ("sq_REGOPERATIVODETTAGLIO".nextval,
   nv.regrdl,
   nv.dataora,
   nv.descrizione,
   0,
   nv.oidstatooperativo,
   nv.utente);

commit;
end if;
end loop;

 /*OPEN V_CURSOR FOR
  select
  x.oidrisorseteam,
  y.REGRDL,
  y.UTENTE,
  y.DATAORA,
  y.OIDSTATOOPERATIVO,
  y.DESCRIZIONE
   from

(select
--replace(REPLACE(r."TargetKey", ''''),'[Int32]') as RDL_CODICE,
rg.oid|| a."UserName"||to_char(a."ModifiedOn",'dd/mm/yyyy hh24:mi:ss')  as codex,
rg.oid as REGRDL,
--a."OperationType",
a."UserName" as UTENTE,
a."ModifiedOn" as DATAORA,

so.oid as oidstatooperativo,
case when a."PropertyName"='UltimoStatoOperativo'
then
Nvl(   a."NewValue",'nd')
else
null
end
as SOPERATIVO_DESC,

case when a."PropertyName"='UltimoStatoOperativo'
then
Nvl( a."OldValue",'nd')
else
null
end
as SOPERATIVO_DESC_DESC_OLD,

case when  a."OldValue"='N/A'
  then
    a."NewValue"
  else
    'Da ' ||a."OldValue"||' A ' || a."NewValue"
  end as descrizione,

Nvl(   a."PropertyName",'nd') as PropertyName
from "AuditDataItemPersistent" a,
"XPWeakReference"         r,
"XPObjectType"            t,
rdl rr,
regrdl rg,
statooperativo so


where r."TargetType" = t."OID"
and a."AuditedObject" = r."Oid"
and t."OID" = 25 -- TypeObj --25 = RDL;
--   and t."TargetType" = 25 -----25 = RDL;
and r."TargetKey" = '[Int32]''' || iOidObj || '''' --iOidObj '[Int32]'''||'48137'||'''' --    and r."TargetKey" like '%48137%' -- TargetKey
and a."ModifiedOn" > sysdate - 1000
and replace(REPLACE(r."TargetKey", ''''),'[Int32]')=rr.oid
and rr.regrdl=rg.oid
and a."PropertyName"  in
('UltimoStatoOperativo'
)
and so.codstato= a."NewValue"
) y,

(select
rg.oid|| a."UserName"||to_char(a."ModifiedOn",'dd/mm/yyyy hh24:mi:ss')  as codex,
rg.oid as REGRDL,
a."ModifiedOn" as DATAORA,
ris.oid as oidrisorseteam,
a."UserName" as UTENTESO,
case when a."PropertyName"= 'RisorseTeam'
then
Nvl(  a."NewValue",'nd')
else
null
end
as RISORSETEAM_DESC,
case when a."PropertyName"= 'RisorseTeam'
then
Nvl( a."OldValue",'nd')
else
null
end
as RISORSETEAM_DESC_OLD
from "AuditDataItemPersistent" a,
"XPWeakReference"         r,
"XPObjectType"            t,
rdl rr,
regrdl rg,
(select
r.nome||' '||r.cognome||'('||r.cognome||' - '||rs.anno||')' as strrs,
rs.oid
from risorseteam rs, risorse r
where r.oid=rs.risorsacapo) ris

where r."TargetType" = t."OID"
and a."AuditedObject" = r."Oid"
and t."OID" = 25 -- TypeObj --25 = RDL;
--   and t."TargetType" = 25 -----25 = RDL;
and r."TargetKey" = '[Int32]''' || iOidObj || '''' -- iOidObj '[Int32]'''||'48137'||'''' --    and r."TargetKey" like '%48137%' -- TargetKey
and a."ModifiedOn" > sysdate - 1000
and replace(REPLACE(r."TargetKey", ''''),'[Int32]')=rr.oid
and rr.regrdl=rg.oid
and a."PropertyName"  in
('RisorseTeam')
and ris.strrs= a."NewValue" ) x
where x.codex(+)=y.codex
order by y.dataora asc
;*/


 --IO_CURSOR := V_CURSOR;

    --  EXCEPTION
    --    WHEN OTHERS THEN
    -- ROLLBACK;
    -- DATAORA_successiva := cSSmistamento.dataora;

  end GetAuditData_REGRDL_SO;

  procedure aggdeltasmistamento(icodregrdl in number) is
    vdataprecedente date := null;
  begin
    for nn in (select d.*, d.rowid
                 from regsmistamentodett d
                where d.regrdl = icodregrdl
                order by d.dataora desc) loop
      if vdataprecedente is not null then
        update regsmistamentodett r
          set r.deltatempo =(trunc((vdataprecedente - nn.dataora),6 )* 24 * 60)

         where r.rowid = nn.rowid;
        commit;
      else
        update regsmistamentodett r
           set r.deltatempo = 0
         where r.rowid = nn.rowid;
      end if;
      vdataprecedente := nn.dataora;

    end loop;
    vdataprecedente := null;
  end aggdeltasmistamento;

  procedure aggdeltaoperativo(icodregrdl in number) is
    vdataprecedente date := null;
  begin
    for nn in (select d.*, d.rowid
                 from regoperativodettaglio d
                where d.regrdl = icodregrdl
                order by d.dataora desc) loop
      if vdataprecedente is not null then
        update regoperativodettaglio r
          set r.deltatempo =(trunc((vdataprecedente - nn.dataora),6 )* 24 * 60)

         where r.rowid = nn.rowid;
        commit;
      else
        update regoperativodettaglio r
           set r.deltatempo = 0
         where r.rowid = nn.rowid;
      end if;
      vdataprecedente := nn.dataora;

    end loop;
    vdataprecedente := null;
  end aggdeltaoperativo;


end PK_AuditData;
/
