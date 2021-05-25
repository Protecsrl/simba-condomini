create or replace package PK_APPARATO is

 PROCEDURE clona_apparato(iOidApparato in number,
                           iNumClone    in number,
                           iUser        in varchar,
                           iDescrizione in varchar,
                           oErrorMsg    out varchar);


 FUNCTION GET_OID_APPSCHEDAMP
    Return number  ;          

end PK_APPARATO;
/
create or replace package body PK_APPARATO is

PROCEDURE clona_apparato (iOidApparato in number,
                           iNumClone    in number,
                           iUser        in varchar,
                           iDescrizione in varchar,
                           oErrorMsg    out varchar) is
                           
                           
    
    vNewApparato    number;
    vNewApparatoSmp number;
    vNewCodDesc     varchar2 (20);
    msgerror        varchar (2000);
    error EXCEPTION;              
    
Begin                 

  Begin
          -- creo apparato nuovo
         select "sq_APPARATO".Nextval into vNewApparato from dual;
      
         insert into APPARATO(               oid,
                                             descrizione,
                                             cod_descrizione,
                                             impianto,
                                             stdapparato,
                                             datasheet,
                                             quantita,
                                             kdimensione,
                                             kcondizione,
                                             kubicazione,
                                             ktrasferimento,
                                             kutenza,
                                             utente,
                                             dataupdate,
                                             kguasto,
                                             apparatopadre,
                                             appkcondizione,
                                             sumtemposchedemp,
                                             totalecoefficienti,
                                             appktempo,
                                             matricola,
                                             entitaapparato,
                                             dataunservice,
                                             datainservice,
                                             targhetta,
                                             fluidoprimario)
         
        (select  vNewApparato, 
                 iDescrizione || 'copia apparato n.'||oid, 
                 a.cod_descrizione, 
                 a.impianto, 
                 a.stdapparato, 
                 null,
                 a.quantita, 
                 a.kdimensione, 
                 a.kcondizione, 
                 a.kubicazione, 
                 a.ktrasferimento, 
                 a.kutenza, 
                 iUser, 
                 sysdate,
                 a.kguasto, 
                 a.apparatopadre,
                 a.appkcondizione,
                 a.sumtemposchedemp,
                 a.totalecoefficienti,
                 a.appktempo,
                 a.matricola,
                 a.entitaapparato,
                 a.dataunservice,
                 a.datainservice,
                 a.targhetta,
                 a.fluidoprimario
                                
                                
                 from apparato a
                           
                where a.oid = iOidApparato) ;
           exception 
             when others then msgerror:='Errore insert tabella apparato '||sqlerrm;
                  raise error;
           End;
      Begin
                          insert into apparatoschedamp(oid,   
                                                       apparato,
                                                       schedamp,
                                                       codpmp,
                                                       sistema,
                                                       categoria,
                                                       eqstd,
                                                       sottocomponente,
                                                       manutenzione,
                                                       frequenzaopt,
                                                       mansioniopt,
                                                       numman,
                                                       tempoopt,
                                                       utente,
                                                       dataupdate)           
                          (select (pk_edificio.get_oid_appschedamp),  
                                  vNewApparato,
                                  t.oid,
                                  t.codpmp,
                                  t.sistema,
                                  t.categoria,
                                  t.eqstd,
                                  t.sottocomponente,
                                  t.manutenzione,
                                  t.frequenzaopt,
                                  t.mansioniopt,
                                  t.numman,
                                  t.tempoopt,
                                  iUser,
                                  sysdate
                             from SCHEDEMP t
                              where t.eqstd= iOidApparato);
                            exception when others then msgerror:='Errore insert tabella schede '||sqlerrm;
                           raise error;
                     End; 
           
              
           

   
  
      commit;
        
        
        EXCEPTION when error then dbms_output.put_line (Msgerror );
   

end clona_apparato;
  
  
---FUNCTION
FUNCTION GET_OID_APPSCHEDAMP
    Return number  IS
    
     MReturn  number;
    vNewApparatoSmp  number;
    BEGIN 
        select "sq_APPARATOSCHEDAMP".Nextval into vNewApparatoSmp from dual; 
        
        RETURN vNewApparatoSmp;
  END GET_OID_APPSCHEDAMP;
---END FUNCTION
 
 
 
end PK_APPARATO;
/
