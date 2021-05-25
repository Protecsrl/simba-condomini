create or replace PACKAGE PK_AGGIORNA_TEMPI is

procedure AGGIORNA_TEMPI(iOID  in number,
						 iTIPO in varchar2,
						 oMESSAGE out varchar2);

procedure AGGIORNA_APPARATO(iOIDAPPARATO in  number);
procedure AGGIORNA_IMPIANTO(iOIDIMPIANTO number);
procedure AGGIORNA_EDIFICIO(iOIDEDIFICIO number);


end PK_AGGIORNA_TEMPI;
/
create or replace PACKAGE BODY PK_AGGIORNA_TEMPI is

procedure AGGIORNA_TEMPI(iOID  in number,
						 iTIPO in varchar2,
						 oMESSAGE out varchar2) is
begin
	if iTIPO = 'APPARATO'    then
		AGGIORNA_APPARATO(iOID);
	elsif iTIPO = 'IMPIANTO' then
		AGGIORNA_IMPIANTO(iOID);
	elsif iTIPO='EDIFICIO'	 then
		AGGIORNA_EDIFICIO(iOID);
	else
		oMESSAGE := 'Tipo non corretto';	
	end if;	
exception
	when others then
		oMESSAGE:= 'Errore: '||SQLERRM||' Codice: '||SQLCODE;	
end AGGIORNA_TEMPI;

procedure AGGIORNA_APPARATO(iOIDAPPARATO in  number) is
	somma_tempi integer;
	oidImpianto number(38);
	
begin
	
	update apparato a
		set sumtemposchedemp = (select sum(tempoopt)
								   from apparatoschedamp
								  where apparato = iOIDAPPARATO
								  group by apparato)
	where a.oid = iOIDAPPARATO;
	
	select impianto 
	  into oidImpianto
	 from apparato
	where oid=iOIDAPPARATO;

	AGGIORNA_IMPIANTO(oidImpianto);

	
exception
	when NO_DATA_FOUND then
		return;
		-- se non trova l'impianto aggiorna solo l'apparato

end AGGIORNA_APPARATO;

procedure AGGIORNA_IMPIANTO(iOIDIMPIANTO number) is
	
	oidEdificio number(38);
begin
	update impianto a
		set (sumtemposchedemp,
			 countapparati) = (select sum(sumtemposchedemp),count(0)
								   from apparato
								  where impianto = iOIDIMPIANTO
								  group by impianto)
	where a.oid = iOIDIMPIANTO;

	select edificio 
	  into oidEdificio
	 from impianto
	where oid=iOIDIMPIANTO;

	AGGIORNA_EDIFICIO(oidEdificio);
exception
	when NO_DATA_FOUND then
		return;
		-- se non trova l'edificio aggiorna solo l'apparato e l'impianto
end AGGIORNA_IMPIANTO;

procedure AGGIORNA_EDIFICIO(iOIDEDIFICIO number) is

begin
	update edificio a
		set (sumtemposchedemp,
			 countapparati,
			 countimpianti) = (select sum(sumtemposchedemp),
			 						  sum(countapparati),
			 						  count(0)
								   from impianto
								  where edificio = iOIDEDIFICIO
								  group by edificio)
	where a.oid = iOIDEDIFICIO;

end AGGIORNA_EDIFICIO;
end PK_AGGIORNA_TEMPI;	
/