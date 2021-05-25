CREATE OR REPLACE PACKAGE RES1521108.PK_kpi_rendicontazione_chek is

  TYPE T_CURSOR IS REF CURSOR;

  cursor cGetSSmistamento is --pRegRdL number
    select t.regrdl,
           rg.descrizione RegRdL_Descrizione,
           -- 'statosmistamento' tabella,       
           t.oid,
           t.descrizione,
           DECODE(t.statosmistamento,
                  1,
                  'A1',
                  2,
                  'A2',
                  3,
                  'A3',
                  4,
                  'C4',
                  6,
                  'A60',
                  7,
                  'A70',
                  11,
                  'A11',
                  'DATO') ordinestato,
           t.statosmistamento cstatos,
           ss.statosmistamento dstatos,
           t.dataora,
           /*     (
           
           SELECT RGS.DATAORA
             FROM REGSMISTAMENTODETT RGS
            WHERE RGS.REGRDL = T.REGRDL
              AND RGS.DATAORA > T.DATAORA
              AND rownum = 1
            
           
           ) dataoraSUCC,*/
           --   To_char(' ') noteoperative,
           t.risorsateam,
           rt.nome,
           rt.cognome,
           rt.co_descrizione,
           arp.descrizione       AreaPolo_Descrizione,
           p.descrizione         Polo_Descrizione,
           cm.descrizione        commessa_Descrizione,
           cm.centrocosto,
           e.descrizione         edificio_descrizione,
           cg.descrizione        categoria_descrizione,
           t.deltatempo, --t.*  --/*, t.rowid */
           ss.ordine             par,
           E.OID                 oidedificio,
           cm.referentecofely    oidreferentecofely,
           cg.oid                oidcategoria,
           rt.oidrt              oidteamrisorsa,
           rg.data_creazione_rdl,
           t.statosmistamento    oidsmistamento
    
      from REGSMISTAMENTODETT t,
           regrdl rg,
           categoria cg,
           edificio e,
           impianto i,
           apparato a,
           commesse cm,
           areadipolo arp,
           polo p,
           statosmistamento ss,
           (select risorseteam.oid oidrt,
                   r.oid           oidRisorsa,
                   r.azienda,
                   r.nome,
                   r.cognome,
                   co.oid          oid_co,
                   co.descrizione  co_descrizione
              from risorse r, risorseteam, centrooperativo co
             where r.oid = risorseteam.RISORSACAPO
               and co.oid = r.centrooperativo) rt
     where t.statosmistamento = ss.oid
       and t.regrdl = rg.oid
       and cg.oid = rg.categoria
       and rg.apparato = a.oid
       and a.impianto = i.oid
       and i.edificio = e.oid
       and e.commessa = cm.oid
       and cm.areadipolo = arp.oid
       and p.oid = arp.polo
       and t.risorsateam = rt.oidrt(+)
          -- AND rg.datacompletamento is not null
       and rg.ultimostatosmistamento in (2, 3, 4, 5, 6, 8, 9)
       and t.regrdl not in
           (select y.oid
              from regrdl y
             where y.categoria = 4
               and y.data_creazione_rdl <
                   to_date('01/12/2016', 'dd/mm/yyyy'))
    -- and 
    /*4 Lavorazione Conclusa
    5 Richiesta Chiusa 
    6 Sospesa SO
    7 Annullato
    8 Rendicontazione Operativa
    9 Rendicontazione Economica
    10  In Emergenza da Assegnare
    11  Gestione da Sala Operativa*/
    
     order by regrdl, dataora, ordinestato;

  type Tab_cGetSSmistamento is table of cGetSSmistamento%rowtype;

  cursor cGetSOperativo(pRegRdL            number,
                        pcssmist_dataora   date,
                        pdataoraSuccessiva date,
                        Risorsa            varchar2) is --cGetSSmistamento.Dataora%type
    select t.regrdl,
           -- 'statooperativo' tabella,   
           Risorsa as Risorsa,
           t.oid,
           t.descrizione,
           'B' || t.statooperativo ordinestato,
           t.statooperativo cstato,
           so.codstato dstato,
           To_char(t.noteoperative) noteoperative,
           t.flagcompletatoso par,
           t.dataora,
           t.deltatempo
      from REGOPERATIVODETTAGLIO t, statooperativo so
     where t.statooperativo = so.oid
       AND T.REGRDL = pRegRdL ---1368
       AND T.DATAORA >= pcssmist_dataora --To_date('22/11/2016 10:20:01', 'DD/MM/YYYY HH:MI:SS')
       AND T.DATAORA < pdataoraSuccessiva -- To_date('22/11/2016 10:20:36', 'DD/MM/YYYY HH:MI:SS')
    
     order by regrdl, dataora, ordinestato;

  type Tab_cGetSOperativo is table of cGetSOperativo%rowtype;
  procedure KPI_RENDICONTAZIONE(ianno      in varchar,
                                imese      in varchar,
                                isettimana in varchar,
                                iusername  in varchar2);
  procedure InserKPIALavori(pindice  in number,
                            cssmist  IN cGetSSmistamento%ROWTYPE,
                            csoper   IN cGetSOperativo%ROWTYPE,
                            pmessage in out varchar2);

  procedure InserKPIALavori(pindice  in number,
                            cssmist  IN cGetSSmistamento%ROWTYPE,
                            Risorsa  in varchar2,
                            pmessage in out varchar2);

end PK_kpi_rendicontazione_chek;
/
create or replace package body res1521108.PK_kpi_rendicontazione_chek is

  -- kpi 

  procedure KPI_RENDICONTAZIONE(ianno      in varchar,
                                imese      in varchar,
                                isettimana in varchar,
                                iusername  in varchar2) is
    indice             number := 0;
    Conta              number := 0;
    Messaggio          varchar2(4000) := null;
    indcor             varchar2(4000) := null;
    Risorsa            varchar2(4000) := null;
    valcor             number;
    DATAORA_successiva date;
  begin
    delete kpi_avanzamentolavori;
    indice  := 0;
    Risorsa := '';
    for cSSmistamento in cGetSSmistamento loop
      -- recupero la data successiva
      begin
        select x.DATAORA
          into DATAORA_successiva
          from (SELECT RGS.DATAORA,
                       row_number() over(partition by rgs.regrdl order by RGS.DATAORA) rn
                  FROM REGSMISTAMENTODETT RGS
                 WHERE RGS.REGRDL = cSSmistamento.regrdl --1368 --T.REGRDL
                   AND RGS.DATAORA > cSSmistamento.dataora
                --- To_date('22/11/2016 10:20:01', 'DD/MM/YYYY HH:MI:SS')              
                ) x
         where rn = 1;
      EXCEPTION
        WHEN OTHERS THEN
          -- ROLLBACK;
          DATAORA_successiva := cSSmistamento.dataora;
      END;
    
      -- recipero il numero di operatività
      select count(*)
        into Conta
        from REGOPERATIVODETTAGLIO t, statooperativo so
       where t.statooperativo = so.oid
         AND T.REGRDL = cSSmistamento.regrdl --1368 --
         AND T.DATAORA >= cSSmistamento.dataora -- To_date('22/11/2016 10:20:01', 'DD/MM/YYYY HH:MI:SS')            
         AND T.DATAORA < DATAORA_successiva --cSSmistamento.Dataorasucc --To_date('22/11/2016 10:20:36', 'DD/MM/YYYY HH:MI:SS')     
      ;
    
      if (cSSmistamento.Cstatos = 2 or cSSmistamento.Cstatos = 11) then
        Risorsa := cSSmistamento.nome || ' ' || cSSmistamento.cognome || '(' ||
                   cSSmistamento.risorsateam || ')';
      end if;
      if (cSSmistamento.Cstatos = 1) then
        Risorsa := '';
      end if;
      if Conta = 0 then
        --- non ci sono stato operativi
        indice := indice + 1;
        pk_kpi_rendicontazione.inserkpialavori(indice,
                                               cSSmistamento,
                                               Risorsa,
                                               Messaggio);
      
        commit;
      else
      
        for cSOperativo in cGetSOperativo(cSSmistamento.Regrdl, --cssmist.regrdl,
                                          cSSmistamento.Dataora,
                                          DATAORA_successiva,
                                          Risorsa) loop
        
          indice := indice + 1;
        
          pk_kpi_rendicontazione.inserkpialavori(indice,
                                                 cSSmistamento,
                                                 cSOperativo,
                                                 Messaggio);
          commit;
        end loop;
      end if;
      if (cSSmistamento.Cstatos = 4) then
      
        update KPI_AVANZAMENTOLAVORI k
           set k.Idcompleto = 'ok'
         WHERE K.Codregrdl =To_number( cSSmistamento.Regrdl);
        COMMIT;
      end if;
    
    end loop;
  
  end KPI_RENDICONTAZIONE;

  --- Crea riga dove non ci sono stati operativi
  procedure InserKPIALavori(pindice  in number,
                            cssmist  IN cGetSSmistamento%ROWTYPE,
                            csoper   IN cGetSOperativo%ROWTYPE,
                            pmessage in out varchar2) is
  
  begin
  
    insert into kpi_avanzamentolavori
      (oid,
       codregrdl,
       regrdl,
       categoria,
       ordine,
       risorsateam,
       statosmistamento,
       statooperativo,
       descr_cg_smistamento,
       descr_cg_operativo,
       DATAORA_CGSMISTAMENTO,
       DELTATEMPO_CGSMISTAMENTO,
       DATAORA_CGOPERATIVO,
       DELTATEMPO_CGOPERATIVO,
       settimana,
       anno,
       mese,
       centrooperativo,
       areadipolo,
       polo,
       commessa,
       edificio,
       ccosto,
       note,
       oidedificio,
       oidreferentecofely,
       oidcategoria,
       oidteamrisorsa,
       kpi_avanzamentolavori.data_creazione_rdl,
       kpi_avanzamentolavori.oidsmistamento,
       kpi_avanzamentolavori.Idcompleto)
    values
      (pindice,
       cssmist.regrdl,
       cssmist.RegRdL_Descrizione,
       cssmist.categoria_descrizione,
       cssmist.ordinestato || '-' || csoper.ordinestato,
       csoper.risorsa, -- cssmist.nome || ' '|| cssmist.cognome   || '('||  cssmist.risorsateam || ')',
       cssmist.dstatos,
       csoper.dstato,
       cssmist.descrizione,
       csoper.descrizione,
       cssmist.dataora,
       cssmist.deltatempo,
       csoper.dataora,
       csoper.deltatempo,
       to_char(NVL(csoper.dataora, cssmist.dataora), 'WI'),
       to_char(NVL(csoper.dataora, cssmist.dataora), 'YYYY'),
       to_char(NVL(csoper.dataora, cssmist.dataora), 'MM'),
       cssmist.co_descrizione,
       cssmist.areapolo_descrizione,
       cssmist.polo_descrizione,
       cssmist.commessa_descrizione,
       cssmist.Edificio_Descrizione,
       cssmist.centrocosto,
       cssmist.oid,
       cssmist.oidedificio,
       cssmist.oidreferentecofely,
       cssmist.oidcategoria,
       cssmist.oidteamrisorsa,
       cssmist.data_creazione_rdl,
       cssmist.oidsmistamento,
       'No');
  
    commit;
  
  end InserKPIALavori;

  --- Crea riga attività dettaglio da giro cluster
  procedure InserKPIALavori(pindice  in number,
                            cssmist  IN cGetSSmistamento%ROWTYPE,
                            Risorsa  in varchar2,
                            pmessage in out varchar2) is
  begin
    /*     Risorsa := '';
    if (cssmist.risorsateam != null) then
        Risorsa :=  cssmist.nome || ' ' || cssmist.cognome || '(' || cssmist.risorsateam || ')';
    end if;*/
    insert into kpi_avanzamentolavori
      (oid,
       codregrdl,
       regrdl,
       categoria,
       ordine,
       risorsateam,
       statosmistamento,
       statooperativo,
       descr_cg_smistamento,
       descr_cg_operativo,
       DATAORA_CGSMISTAMENTO,
       DELTATEMPO_CGSMISTAMENTO,
       DATAORA_CGOPERATIVO,
       DELTATEMPO_CGOPERATIVO,
       settimana,
       anno,
       mese,
       centrooperativo,
       areadipolo,
       polo,
       commessa,
       edificio,
       ccosto,
       note,
       oidedificio,
       oidreferentecofely,
       oidcategoria,
       oidteamrisorsa,
       kpi_avanzamentolavori.data_creazione_rdl,
       kpi_avanzamentolavori.Idcompleto)
    values
      (pindice,
       cssmist.regrdl,
       cssmist.RegRdL_Descrizione,
       cssmist.categoria_descrizione,
       cssmist.ordinestato,
       Risorsa, --cssmist.nome || ' ' || cssmist.cognome || '(' || cssmist.risorsateam || ')',
       cssmist.dstatos,
       null,
       cssmist.descrizione,
       null,
       cssmist.dataora,
       cssmist.deltatempo,
       null,
       null,
       to_char(NVL(cssmist.dataora, sysdate), 'WI'),
       to_char(NVL(cssmist.dataora, sysdate), 'YYYY'),
       to_char(NVL(cssmist.dataora, sysdate), 'MM'),
       cssmist.co_descrizione,
       cssmist.areapolo_descrizione,
       cssmist.polo_descrizione,
       cssmist.commessa_descrizione,
       cssmist.Edificio_Descrizione,
       cssmist.centrocosto,
       cssmist.oid,
       cssmist.oidedificio,
       cssmist.oidreferentecofely,
       cssmist.oidcategoria,
       cssmist.oidteamrisorsa,
       cssmist.data_creazione_rdl,
       'No');
    commit;
  
  end InserKPIALavori;

end PK_kpi_rendicontazione_chek;
/
