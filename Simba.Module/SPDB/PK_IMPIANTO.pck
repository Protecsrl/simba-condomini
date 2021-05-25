CREATE OR REPLACE PACKAGE PK_IMPIANTO is

  PROCEDURE get_cod_impianto(iOidEdificio in number,
                             iOidSistema  in number,
                             oCodImpianto out varchar2,
                             oErrorMsg out varchar2);
                             
                             
  PROCEDURE clona_impianto(iOidImpianto in number,
                           iNumClone    in number,
                           iUser        in varchar,
                           iDescrizione in varchar,
                           oErrorMsg    out varchar);
                             
                        
                        
  PROCEDURE carica_impianto(oCodImpianto out varchar2,
                            oErrorMsg out varchar2);     
                            
                            
  FUNCTION GET_OID_APPSCHEDAMP
    Return number  ;                  
                        
end PK_IMPIANTO;
/
create or replace package body PK_IMPIANTO is

  PROCEDURE get_cod_impianto(iOidEdificio in number,
                             iOidSistema  in number,
                             oCodImpianto out varchar2,
                             oErrorMsg out varchar2) is

  progressive number;
  cod_edificio varchar2(10);
  cod_sistema varchar2(10);
  error EXCEPTION;

  begin
    begin
         select edificio.cod_descrizione
         into cod_edificio
         from edificio where oid=iOidEdificio;
         
         select sistema.cod_descrizione
         into cod_sistema
         from sistema where oid=iOidSistema;

         oCodImpianto:=cod_edificio||'_'||cod_sistema;
         
         begin
               select to_number(SUBSTR((impianto.cod_descrizione), LENGTH(impianto.cod_descrizione)-2, 3))+1
                 into progressive
                  from impianto where impianto.cod_descrizione like oCodImpianto||'%';
         EXCEPTION
           WHEN OTHERS THEN
             oCodImpianto:=oCodImpianto||'_01';
             return;
         END;

         IF progressive is null THEN
             oCodImpianto:=oCodImpianto||'_01';
         ELSE
             IF progressive<10 THEN
               oCodImpianto:=oCodImpianto||'_0'||progressive;
             ELSE/*
                   IF progressive<100 THEN
                     oCodImpianto:=oCodImpianto||'_0'||progressive;
                   ELSE*/
                     oCodImpianto:=oCodImpianto||'_'||progressive;
                   /*END IF;*/
             END IF;
         END IF;

     EXCEPTION
       /*WHEN error THEN
         oErrorMsg:='Errore : Valorizzare i campi Categoria, Sistema, Apparato.';*/
       WHEN OTHERS THEN
         oErrorMsg:='Errore imprevisto in PK_CODIMPIANTO : '||SQLERRM;
     END;
     
  end get_cod_impianto;

-------------------------------------------------------------------------------procedura clona impianto

PROCEDURE clona_impianto(iOidImpianto in number,
                           iNumClone    in number,
                           iUser        in varchar,
                           iDescrizione in varchar,
                           oErrorMsg    out varchar) is
  
    progressive     number;
    vNewImpianto    number;
    vNewApparato    number;
    vNewApparatoSmp number;
    vNewCodDesc     varchar2 (20);
    msgerror        varchar (2000);
    error EXCEPTION;
  
Begin
     Begin
          -- creo impianto nuovo
          select "sq_IMPIANTO".Nextval into vNewImpianto from dual;
      
          insert into impianto ( oid,
                                 descrizione,
                                 cod_descrizione,
                                 edificio,
                                 commessa,
                                 zona,
                                 presidio,
                                 "CLUSTER",
                                 sistema,
                                 clusterimpianti,
                                 mpghostitinerante,
                                 countapparati,
                                 sumtemposchedemp,
                                 datasheet)
         
         (select vNewImpianto, 
                 iDescrizione || 'copia impianto n.'||iOidImpianto, 
                 i.cod_descrizione, 
                 i.edificio, 
                 i.commessa, 
                 i.zona, 
                 i.presidio,
                 i."CLUSTER",
                 i.sistema,
                 i.clusterimpianti,
                 i.mpghostitinerante,
                 i.countapparati,
                 i.sumtemposchedemp,
                 null  
            from impianto i
           where i.oid = iOidImpianto) ;
           exception when others then msgerror:='Errore insert tabella impianto '||sqlerrm;
           raise error;
      End;
      Begin
           ---   scorri apparato e crea apparato
           for vListApp in (select a.oid as apparato, i.oid as impianto, a.stdapparato
                              from APPARATO a, IMPIANTO i
                             where a.impianto = i.oid
                               and i.oid      =iOidImpianto
                               order by a.oid) 
           loop
              Begin  
                   Begin            
                      select "sq_APPARATO".Nextval into vNewApparato from dual;
                       exception when others  then msgerror:='Errore sequence apparato '||sqlerrm;
                       raise error;
                   End;
                   
                   Begin 
                        insert into APPARATO(oid,
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
                        (select vNewApparato, 
                                a.descrizione || 'copia apparato n.'||oid, 
                                a.cod_descrizione, 
                                vNewImpianto, 
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
                          where a.oid = vListApp.apparato); 
                        
                           exception when others then msgerror:='Errore insert tabella apparato '||sqlerrm;
                           raise error;
                     End;
                     Begin
                          insert into apparatoschedamp(oid,   ---vNewApparatoSmp
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
                            where t.eqstd= vListApp.Stdapparato);
                            exception when others then msgerror:='Errore insert tabella schede '||sqlerrm;
                           raise error;
                     End; 
              End;
           end loop;   
       End;    
  
        commit;
        
        
        EXCEPTION when error then dbms_output.put_line (Msgerror );
  
 
  end clona_impianto;



---------------------------------------------------------------------------end procedura clona impianto

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
--------------------------------------------------------------------------



---------------------------------------------------------------------------

PROCEDURE carica_impianto(oCodImpianto out varchar2,
                            oErrorMsg out varchar2) is

  progressive number :=1;
  cod_edificio varchar2(10);
  cod_sistema varchar2(10);
  error EXCEPTION;
  
  begin
    for vEdifici in (select *
                            from edificio e
                            where e.oid>6) loop
     PK_CODIMPIANTO.get_cod_impianto(vEdifici.Oid, 2, oCodImpianto, oErrorMsg);                       
insert into impianto
  (oid, descrizione, cod_descrizione, edificio, commessa)
values
  ("sq_IMPIANTO".nextval, 'Imp_'||progressive, oCodImpianto , vEdifici.Oid, 5);
progressive :=progressive+1;
    end loop;
     

    
  end carica_impianto;
  
end PK_IMPIANTO;
/
