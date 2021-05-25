CREATE OR REPLACE PACKAGE PK_Mobile_accont is

  TYPE T_CURSOR IS REF CURSOR;

  procedure sp_SetToken(iUser       IN varchar2,
                        iSessioneID in varchar2,
                        oMessaggio  OUT varchar2);

  procedure sp_GetToken(iUser       IN varchar2,
                        iSessioneID in varchar2,
                        IO_CURSOR   IN OUT T_CURSOR,
                        oMessaggio  OUT varchar2);

  procedure sp_getTokenVaribili(iUser       IN varchar2,
                                iSessioneID in varchar2,
                                IO_CURSOR   IN OUT T_CURSOR,
                                oMessaggio  OUT varchar2);

end PK_Mobile_accont;
/
CREATE OR REPLACE PACKAGE BODY PK_Mobile_accont is

  procedure sp_SetToken(iUser       IN varchar2,
                        iSessioneID in varchar2,
                        oMessaggio  OUT varchar2) is
    iDDataTRisorse date := sysdate;
  
    Oid_Risorse number;
  
  begin
  
    insert into tbl_sql
      (s_sql, data)
    values
      ('sp_SetToken' || CHR(13) || CHR(10) || 'iUser:' || iUser ||
       'iSessioneID:' || iSessioneID || 'oMessaggio:' || oMessaggio,
       sysdate);
    commit;
    commit;
  
    oMessaggio := '';
    select r.oid
      into Oid_Risorse
      from "SecuritySystemUser" t,
           risorse              r,
           assrisorseteam       a,
           risorseteam          rt --,
    -- statodisponibilita   sd
     where "UserName" = iUser
          --and dbms_lob.compare(t."StoredPassword",iPassword) = 0
       and r.securityuserid = t."Oid"
       and a.risorsa = r.oid
       and a.risorseteam = rt.oid;
  
    if (Oid_Risorse > 0) then
      update risorse
         set risorse.sessionid = iSessioneID
       where risorse.oid = Oid_Risorse;
      commit;
    end if;
  
  exception
    when no_data_found then
      commit;
    when others then
      oMessaggio := 'Errore: ' || SQLERRM || ' --  Codice: ' || SQLCODE;
    
  end sp_SetToken;
  /********************************************************/

  procedure sp_GetToken(iUser       IN varchar2,
                        iSessioneID in varchar2,
                        IO_CURSOR   IN OUT T_CURSOR,
                        oMessaggio  OUT varchar2) is
    iDDataTRisorse date := sysdate;
  
    Oid_Risorse number;
    V_CURSOR    T_CURSOR;
  begin
  
    insert into tbl_sql
      (s_sql, data)
    values
      ('sp_GetToken' || CHR(13) || CHR(10) || 'iUser:' || iUser ||
       'iSessioneID:' || iSessioneID || 'oMessaggio:' || oMessaggio,
       sysdate);
    commit;
  
    oMessaggio := '';
  
    OPEN V_CURSOR FOR
    
      select count(*) attivo
        into Oid_Risorse
        from "SecuritySystemUser" t,
             risorse              r,
             assrisorseteam       a,
             risorseteam          rt --,
      -- statodisponibilita   sd
       where "UserName" = iUser
         and r.securityuserid = t."Oid"
         and a.risorsa = r.oid
         and a.risorseteam = rt.oid
         and r.sessionid = iSessioneID;
  
    IO_CURSOR := V_CURSOR;
  
  exception
    when no_data_found then
      commit;
    when others then
      oMessaggio := 'Errore: ' || SQLERRM || ' --  Codice: ' || SQLCODE;
    
  end sp_getToken;

  procedure sp_getTokenVaribili(iUser       IN varchar2,
                                iSessioneID in varchar2,
                                IO_CURSOR   IN OUT T_CURSOR,
                                oMessaggio  OUT varchar2) is
    iDDataTRisorse date := sysdate;
  
    Oid_Risorse number;
    V_CURSOR    T_CURSOR;
  begin
  
    insert into tbl_sql
      (s_sql, data)
    values
      ('sp_GetToken' || CHR(13) || CHR(10) || 'iUser:' || iUser ||
       'iSessioneID:' || iSessioneID || 'oMessaggio:' || oMessaggio,
       sysdate);
    commit;
  
    oMessaggio := '';
  
    OPEN V_CURSOR FOR
      select t."UserName",
             r.sessionid,
            Nvl(rt.regrdl,0) AS regrdl,
           --  rg.ultimostatooperativo,
              Nvl( rg.ultimostatooperativo,0) AS ultimostatooperativo,
             
             case
               when rt.regrdl is not null and (rg.ultimostatooperativo = 2 or
                    rg.ultimostatooperativo = 3) then
                'True'
               else
                'False'
             end VisCompletaAttivita,
             
             case
               when rt.regrdl is not null and (rg.ultimostatooperativo = 1 or
                    rg.ultimostatooperativo = 4) then
                'True'
               else
                'False'
             end VisArrivoInSito,
             
             case
               when rt.regrdl is not null and (rg.ultimostatooperativo = 2 or
                    rg.ultimostatooperativo = 1 or
                    rg.ultimostatooperativo = 4) then
                'True'
               else
                'False'
             end VisDichiarazioneFineGiornata,
             
             case
               when rt.regrdl is not null and rg.ultimostatooperativo = 3 then
                'True'
               else
                'False'
             end VisFineGiornataLavorativa,
             case
               when rt.regrdl is not null and (rg.ultimostatooperativo = 2 or
                    rg.ultimostatooperativo = 1 or
                    rg.ultimostatooperativo = 5) then
                'True'
               else
                'False'
             end VisTrasfPerAcquistoMateriali,
             
             case
               when rt.regrdl is not null then
                'True'
               else
                'False'
             end RisorsaInLavorazione,
             
             count(*) attivo
      --into Oid_Risorse
        from "SecuritySystemUser" t,
             risorse              r,
             assrisorseteam       a,
             risorseteam          rt,
             regrdl               rg
      --,
      -- statodisponibilita   sd
       where t."UserName" = iUser --'OP' --
         and r.securityuserid = t."Oid"
         and a.risorsa = r.oid
         and a.risorseteam = rt.oid
         and rg.oid(+) = rt.regrdl
         and r.sessionid = iSessioneID
       group by t."UserName",
                r.sessionid,
                rt.regrdl,
                rg.ultimostatooperativo;
  
    IO_CURSOR := V_CURSOR;
  
  exception
    when no_data_found then
      commit;
    when others then
      oMessaggio := 'Errore: ' || SQLERRM || ' --  Codice: ' || SQLCODE;
    
  end sp_getTokenVaribili;

end PK_Mobile_accont;
/
