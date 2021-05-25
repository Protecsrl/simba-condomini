CREATE OR REPLACE PACKAGE body PK_MISURE is

procedure POPOLA_DET_MISURE(iREG_MISURE IN NUMBER,
                            iIMPIANTO IN NUMBER,
                            oMESSAGE OUT VARCHAR2) is
begin
  oMESSAGE := '';
  insert into regmisuredettaglio 
  (OID,VALORE,
  flagmanutenzione,
  NOTE,REGMISURE,APPARATO,STDAPPARATO,dettagliomismaster)
 (select
         "sq_REGMISUREDETTAGLIO".nextval as oid,
         null as valore,
         0 as flag_manutenzione,
         '' as note,
         iREG_MISURE as reg_misure, -- da parametro
         dmm.apparato as apparato,
         mm.stdapparato as stdapparato,
         DMM.OID as DETTAGLIO_MIS_MASTER
  from DETTAGLIOMISMASTER DMM,
       MISMASTER MM,
       MISUREUNITAMISURA MUM,
       APPARATO A
 where DMM.MASTER = MM.OID
   and MUM.OID = MM.Unitamisura
   and DMM.APPARATO = A.OID
   and  A.IMPIANTO = iIMPIANTO --(che mi ha dato il registro)
  and DMM.OID not in (
      select aa.dettagliomismaster
        from REGMISUREDETTAGLIO aa
        where aa.regmisure = iREG_MISURE  ));
        commit;
EXCEPTION
  when others then
     oMESSAGE :=   'Errore: ' ||SQLERRM || ' Codice: '||SQLCODE ;
end POPOLA_DET_MISURE;


end   PK_MISURE;
