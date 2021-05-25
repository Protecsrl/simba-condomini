CREATE OR REPLACE PACKAGE PK_EDIFICIO is

  PROCEDURE get_cod_edificio(iOidCommessa in number,
                             oCodEdificio out varchar2,
                             oErrorMsg    out varchar2);

  PROCEDURE clona_edificio(iOidEdificio in number,
                           iSession     in varchar,
                           iUser        in varchar,
                           iNumClone    in number,
                           iDescrizione in varchar,
                           iOidIndirizzo in number,
                           oErrorMsg    out varchar);

  FUNCTION GET_OID_APPSCHEDAMP
    Return number  ;


end PK_EDIFICIO;
/
create or replace package body PK_EDIFICIO is

  PROCEDURE get_cod_edificio(iOidCommessa in number,
                             oCodEdificio out varchar2,
                             oErrorMsg    out varchar2) is
  
    progressive number;
    error EXCEPTION;
  
  begin
    begin
      select centrocosto
        into oCodEdificio
        from commesse
       where oid = iOidCommessa;
    
      begin
        select to_number(SUBSTR((edificio.cod_descrizione),
                                LENGTH(edificio.cod_descrizione) - 2,
                                3)) + 1
          into progressive
          from edificio
         where edificio.cod_descrizione like oCodEdificio || '%';
      EXCEPTION
        WHEN OTHERS THEN
          oCodEdificio := oCodEdificio || '_01';
          return;
      END;
    
      IF progressive is null THEN
        oCodEdificio := oCodEdificio || '_01';
      ELSE
        IF progressive < 10 THEN
          oCodEdificio := oCodEdificio || '_0' || progressive;
        ELSE
          /*
                             IF progressive<100 THEN
                               oCodEdificio:=oCodEdificio||'_0'||progressive;
                             ELSE*/
          oCodEdificio := oCodEdificio || '_' || progressive;
          /*END IF;*/
        END IF;
      END IF;
    
    EXCEPTION
      /*WHEN error THEN
      oErrorMsg:='Errore : Valorizzare i campi Categoria, Sistema, Apparato.';*/
      WHEN OTHERS THEN
        oErrorMsg := 'Errore imprevisto in PK_CODEDIFICIO : ' || SQLERRM;
    END;
  
  end get_cod_edificio;

  PROCEDURE clona_edificio(iOidEdificio in number,
                           iSession     in varchar,
                           iUser        in varchar,
                           iNumClone    in number,
                           iDescrizione in varchar,
                           iOidIndirizzo in number,
                           oErrorMsg    out varchar) is
  
    progressive  number;
    vNewEdificio number;
    vNewImpianto  number;
    vNewApparato number;
    vNewApparatoSmp number;
    error EXCEPTION;
  
  begin

    -- creo edificio nuovo
      select "sq_EDIFICIO".Nextval into vNewEdificio from dual;
     insert into edificio
          (oid,
           descrizione,
           cod_descrizione,
           indirizzo,
           utente,
           dataupdate,
           clusterimpianti,
           countapparati,
           sumtemposchedemp,
           countimpianti,
           datasheet)
          
       select vNewEdificio, 
              iDescrizione || 'copia', 
              e.cod_descrizione, 
              e.indirizzo, 
              iUser, 
              sysdate,
              e.clusterimpianti,
              e.countapparati,
              e.sumtemposchedemp,
              e.countimpianti,
              null
       from edificio e
       where e.oid = iOidEdificio
         ;
    -- scorro impianti
      for vListImp in (
                       
                       select i.oid as impianto, e.oid as edificio
                         from IMPIANTO i, EDIFICIO e
                        where i.edificio = e.oid
                          and e.oid = iOidEdificio) loop

        select "sq_IMPIANTO".Nextval into vNewImpianto from dual;
      
        insert into impianto
          (oid,
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
          
          select vNewImpianto, 
                 i.descrizione || 'copia', 
                 i.cod_descrizione, 
                 vNewEdificio, 
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
          where i.oid = vListImp.Impianto;
          
    
      ---   scorri apparato e crea apparato
        for vListApp in (  
                         
                         select a.oid as apparato, i.oid as impianto, e.oid as edificio,  a.stdapparato
                            from APPARATO a, IMPIANTO i, EDIFICIO e
                            where a.impianto = i.oid
                            and i.edificio = e.oid
                            and e.oid = iOidEdificio
                            and i.oid = vListImp.Impianto) loop
                            
      select "sq_APPARATO".Nextval into vNewApparato from dual;

      
        insert into APPARATO
          (oid,
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
          (
          select vNewApparato, 
                 a.descrizione || 'copia', 
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
          where a.oid = vListApp.Apparato);
            --         
          ----   insert schede mp   
          -- select "sq_APPARATOSCHEDAMP".Nextval into vNewApparatoSmp from dual;            
           insert into apparatoschedamp 
           (oid,   ---vNewApparatoSmp
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
                                  
           select ( 
           pk_edificio.get_oid_appschedamp),  
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
           where t.eqstd= vListApp.Stdapparato;
          ----------------------------- -----  --------                       
        end loop; 
                            
        end loop; 
        
        ---------------------------------------------------------------------------
        
        commit;
        
        EXCEPTION
        /*WHEN error THEN
        oErrorMsg:='Errore : Valorizzare i campi Categoria, Sistema, Apparato.';*/
      WHEN
      OTHERS THEN oErrorMsg := 'Errore imprevisto in PK_CODEDIFICIO : ' ||  SQLERRM;
  
  
  end clona_edificio;
  
    FUNCTION GET_OID_APPSCHEDAMP
    Return number  IS
    
     MReturn  number;
    vNewApparatoSmp  number;
    BEGIN 
select "sq_APPARATOSCHEDAMP".Nextval into vNewApparatoSmp from dual; 
       
        
    RETURN vNewApparatoSmp;
  END GET_OID_APPSCHEDAMP;

end PK_EDIFICIO;
/
