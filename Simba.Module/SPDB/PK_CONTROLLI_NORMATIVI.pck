create or replace PACKAGE PK_CONTROLLI_NORMATIVI is



procedure SHIFT_CTRL_NORM (iOidControlloNormativo IN  number,
						   oMessage               OUT varchar2);


procedure CLONA_DESTINATARIO(iOid       IN  number,
					         iNome      IN  varchar2,
					         iCognome   IN  varchar2,
                             iEmail     IN  varchar2,
                             oMessaggio OUT varchar2);
end PK_CONTROLLI_NORMATIVI;
/
create or replace PACKAGE BODY PK_CONTROLLI_NORMATIVI is

procedure SHIFT_CTRL_NORM (iOidControlloNormativo IN  number,
						   oMessage               OUT varchar2) is

begin
	insert into CONTROLLI_NORMATIVI (OID,DESCRIZIONE,EDIFICIO,IMPIANTO,APPARATO,FREQUENZA,DATA_INIZIO) 
	select "sq_CONTROLLI_NORMATIVI".nextval,CM.DESCRIZIONE,EDIFICIO,IMPIANTO,APPARATO,FREQUENZA,(DATA_COMPLETAMENTO + (365/CADENZE_ANNO))
  	  from CONTROLLI_NORMATIVI CM, FREQUENZE F
     where CM.OID = iOidControlloNormativo
       and DATA_COMPLETAMENTO is not null
       and F.OID = CM.FREQUENZA;
	
	update CONTROLLI_NORMATIVI 
	   set OID_TODO = "sq_CONTROLLI_NORMATIVI".currval
	 where OID = iOidControlloNormativo;

	commit; 
exception 
	when others then
		rollback;
		oMessage  :=   'Errore: ' ||SQLERRM || ' - Codice: '||SQLCODE ;
    	RAISE; 
end SHIFT_CTRL_NORM;
/****************************************************************/
procedure CLONA_DESTINATARIO(iOid       IN  number,
					         iNome      IN  varchar2,
					         iCognome   IN  varchar2,
                             iEmail     IN  varchar2,
                             oMessaggio OUT varchar2) is 

	CURSOR cur_destinatario(iOid number) is
		SELECT edificio,impianto,destinatari
		  FROM destinatari_cn dcn 
		 WHERE dcn.destinatari = iOid;  

	aOid              number(38);
	aNome             varchar2(50);
	aSecurityRole     char(36);
	aAzioneRole       number(38);
	aCognome          varchar2(150);
	aEmail		      varchar2(100);
	aOid_Dettaglio    number(38);

	type type_tabDestinatari is table of cur_destinatario%ROWTYPE
	index by PLS_INTEGER;

	tabDestinatari type_tabDestinatari;
begin
	select "sq_DESTINATARI".nextval,iNome,SECURITYROLE,AZIONEROLE,iCognome,iEmail
	  into aOid,aNome,aSecurityRole,aAzioneRole,aCognome,aEmail
	  from DESTINATARI
	 where Oid = iOid; 
  	--insert destinatario
  	insert into DESTINATARI (OID,NOME,SECURITYROLE,AZIONEROLE,DESNOMECOGNOME,DESEMAIL) values
  		(aOid,aNome,aSecurityRole,aAzioneRole,aCognome,aEmail);  

  	--select "sp_DESTINATARI_CN".nextval into aOid_Dettaglio from dual;
  	--estrae gli edifici associato al destinatario da clonare
  	open cur_destinatario(iOid);
  	fetch cur_destinatario bulk collect into tabDestinatari;
  	close cur_destinatario;
  	for i in 1..tabDestinatari.count loop
		aOid_Dettaglio := "sq_DESTINATARI_CN".nextval; 
		INSERT INTO destinatari_cn 
		(OID,EDIFICIO,IMPIANTO,DESTINATARI)  
		VALUES
		(aOid_Dettaglio,tabDestinatari(i).edificio,tabDestinatari(i).impianto,aOid);
	end loop;	
	commit;



  --insert allegati destinatario
exception
	when others then
		rollback;
		oMessaggio := 'Errore: '||SQLERRM||'  -  Codice: '||SQLCODE;
end CLONA_DESTINATARIO;


end PK_CONTROLLI_NORMATIVI;	
/