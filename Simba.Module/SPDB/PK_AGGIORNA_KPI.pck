create or replace package PK_AGGIORNA_KPI is

  procedure Job_updkpimtbf;
  procedure Job_updkpimtbf_guasti;
  procedure Job_updkpimtbf_fermi;
  procedure Lancioupdkpimtbf(datein    date,
                             dateout   date,
                             p_msg_out out VARCHAR2);
  procedure updkpimtbf(iOidREGRDL IN number, p_msg_out out VARCHAR2);
  procedure inskpimtbf(iOidREGRDL IN number, p_msg_out out VARCHAR2);

  --procedure updkpimtbf_(iOidREGRDL IN number, p_msg_out out VARCHAR2);
  PROCEDURE InsertToTabellaSQL(sSQL IN varchar2, pMessaggio out VARCHAR2);

end PK_AGGIORNA_KPI;
/
create or replace package body PK_AGGIORNA_KPI is

  procedure Job_updkpimtbf is
    varmsgins  varchar2(300) := 'lancio update kpi_mtbf ';
    v_err_code varchar2(200);
    v_err_msg  varchar2(250);
    datein     date := sysdate - 365;
    --dateout date := sysdate;
  begin
    datein := sysdate - 365;
    -- dateout := sysdate;
  
    pk_aggiorna_kpi.inserttotabellasql(sysdate, v_err_msg);
    Job_updkpimtbf_guasti;
    Job_updkpimtbf_fermi;
    /*    
    for nr in 
      (  
      select oid
      from regrdl t
      where t.data_creazione_rdl >= datein
      and t.oid in 
      (select r.regrdl
      from regsmistamentodett r
      where r.statosmistamento=4
      ) 
      and t.categoria=4               
      order by t.data_creazione_rdl
      ) loop
          inskpimtbf(nr.oid, varmsgins);
          --updkpimtbf(nr.oid, varmsgins);
          
        end loop;*/
    --p_msg_out := varmsgins;
  EXCEPTION
    WHEN OTHERS THEN
      v_err_code := SQLCODE;
      v_err_msg  := SUBSTR(SQLERRM, 1, 200);
      varmsgins  := v_err_code || '-' || v_err_msg;
    
  end Job_updkpimtbf;

  procedure Job_updkpimtbf_guasti is
    varmsgins               varchar2(300) := 'lancio update kpi_mtbf ';
    v_err_code              varchar2(200);
    v_err_msg               varchar2(250);
    datein                  date := sysdate - 365;
    v_apparato              number := 0;
    v_codrdlprec            number := 0;
    v_datacompletamentoprec date := null;
    --dateout date := sysdate;
  begin
    datein := sysdate - 365;
    -- dateout := sysdate;
    delete from kpi_mtbf_guasti;
    commit;
    pk_aggiorna_kpi.inserttotabellasql(sysdate, v_err_msg);
  
    for nr in (
             --------
select t.oid,
       t.apparato oidapparato,
       v.apparatodesc,
       v.oidedificio,
       v.edificiodesc,
       v.oidimpianto,
       v.impiantodesc,
       v.centrocosto,
       v.commessa,
       v.oidreferentecofely,
       v.areadipolo,
       v.tipoapparato,
       nvl((select max(x.datacompletamento) x
             from rdl x
            where x.apparato = t.apparato
              and x.impianto = t.impianto
              and x.datacompletamento <= t.datarichiesta
              and x.datacompletamento is not null),
           t.datarichiesta) as datacompletamentoprec,
       t.datarichiesta,
       t.datacompletamento,
       round(round(t.datacompletamento - t.datarichiesta, 4) ) as mttr,
       round(round(t.datarichiesta -   nvl((select max(x.datacompletamento) x
             from rdl x
            where x.apparato = t.apparato
              and x.impianto = t.impianto
              and x.datacompletamento <= t.datarichiesta
              and x.datacompletamento is not null),
           v.appdatainservice), 4)) as mttf,
       round(round(t.datacompletamento - t.datarichiesta, 4) ) +
       round(round(t.datarichiesta -   nvl((select max(x.datacompletamento) x
             from rdl x
            where x.apparato = t.apparato
              and x.impianto = t.impianto
              and x.datacompletamento <= t.datarichiesta
              and x.datacompletamento is not null),
          v.appdatainservice), 4) ) as mtbf
  from rdl t,
       
       (select a.oid oidapparato,
               a.cod_descrizione || '-' || a.descrizione as apparatodesc,
               st.descrizione || '-' || st.cod_descrizione as tipoapparato,
               i.oid as oidimpianto,
               i.descrizione || '-' || i.cod_descrizione as impiantodesc,
               e.oid as oidedificio,
               e.cod_descrizione as edificiodesc,
               c.wbs as commessa,
               c.referentecofely as oidreferentecofely,
               c.centrocosto,
               ar.descrizione as areadipolo,
               a.datainservice as appdatainservice
          from apparato    a,
               impianto    i,
               edificio    e,
               apparatostd st,
               commesse    c,
               areadipolo  ar
         where st.oid = a.stdapparato
           and i.oid = a.impianto
           and i.edificio = e.oid
           and c.oid = e.commessa
           and ar.oid = c.areadipolo) v

 where t.categoria = 4
   and t.statosmistamento = 4
   and v.oidapparato = t.apparato
   and t.datacompletamento is not null
   and t.datacompletamento > t.datarichiesta
   and (select max(x.datacompletamento) x
          from rdl x
         where x.apparato = t.apparato
           and x.impianto = t.impianto
           and x.datacompletamento <= t.datarichiesta
           and x.datacompletamento is not null) is not null
             and (select max(x.datacompletamento) x
          from rdl x
         where x.apparato = t.apparato
           and x.impianto = t.impianto
           and x.datacompletamento <= t.datarichiesta
           and x.datacompletamento is not null) > to_date('01/01/2015','dd/MM/yyyy')
--   and t.datarichiesta >= datein
 order by t.apparato, t.datacompletamento
------    
               
               ) loop
               
      if v_apparato <> nr.oidapparato then
        commit;
      else
        insert into kpi_mtbf_guasti
          (codice,
           edificio,
           oidedificio,
           impianto,
           oidimpianto,
           apparato,
           oidapparato,
           tipoapparato,
           areadipolo,
           centrocosto,
           settimana,
           mese,
           anno,
           mtbf,
           mttr,
           mttf,
           codrdl,
           codrdl_0,
           data_completamento_precedente,
           datarichiesta,
           datacompletamento,
           oidreferentecofely)
        values
          (to_char(nr.oid),
           nr.edificiodesc,
           nr.oidedificio,
           nr.impiantodesc,
           nr.oidimpianto,
           nr.apparatodesc,
           nr.oidapparato,
           nr.tipoapparato,
           nr.areadipolo,
           nr.centrocosto,
           to_number(to_char(nr.datarichiesta, 'IW')),
           to_number(to_char(nr.datarichiesta, 'MM')),
           to_number(to_char(nr.datarichiesta, 'yyyy')),         
           nr.mtbf, --mtbf
           nr.mttr,
           nr.mttf, --mttf
           nr.oid,
           v_codrdlprec,
           v_datacompletamentoprec,
           nr.datarichiesta,
           nr.datacompletamento,
           nr.oidreferentecofely);
        commit;
      end if;
      v_apparato              := nr.oidapparato;
      v_codrdlprec            := nr.oid;
      v_datacompletamentoprec := nr.datacompletamento;
    
    end loop;
  
    --p_msg_out := varmsgins;
  EXCEPTION
    WHEN OTHERS THEN
      v_err_code := SQLCODE;
      v_err_msg  := SUBSTR(SQLERRM, 1, 200);
      varmsgins  := v_err_code || '-' || v_err_msg;
    
  end Job_updkpimtbf_guasti;

  procedure Job_updkpimtbf_fermi is
    varmsgins         varchar2(300) := 'lancio update kpi_mtbf ';
    v_err_code        varchar2(200);
    v_err_msg         varchar2(250);
    datein            date := sysdate - 365;
    v_apparato        number := 0;
    v_codrdlprec      number := 0;
    v_datariavvioprec date := null;
    --dateout date := sysdate;
  begin
    datein := sysdate - 365;
    -- dateout := sysdate;
    delete from kpi_mtbf_fermi;
    commit;
    pk_aggiorna_kpi.inserttotabellasql(sysdate, v_err_msg);
  
    for nr in (
      ----------
 --------
select t.oid,
       t.apparato oidapparato,
       v.apparatodesc,
       v.oidedificio,
       v.edificiodesc,
       v.oidimpianto,
       v.impiantodesc,
       v.centrocosto,
       v.commessa,
       v.oidreferentecofely,
       v.areadipolo,
       v.tipoapparato,
         t.datarichiesta,
       nvl((select max(x.datariavvio) x
             from rdl x
            where x.apparato = t.apparato
              and x.impianto = t.impianto
              and x.datariavvio <= t.datarichiesta
              and x.datariavvio is not null),
           v.appdatainservice) as datariavvioprec,
       t.datafermo,
       t.datariavvio,
       --   round(round(t.datacompletamento - t.datarichiesta, 4)) as mttr,
       round(round(t.datariavvio - t.datafermo, 4)) as mttr,
       round(round(t.datafermo -
                   nvl((select max(x.datariavvio) x
                         from rdl x
                        where x.apparato = t.apparato
                          and x.impianto = t.impianto
                          and x.datariavvio <= t.datarichiesta
                          and x.datariavvio is not null),
                      v.appdatainservice),
                   4)) as mttf,
       round(round(t.datariavvio - t.datafermo, 4)) +
       round(round(t.datafermo -
                   nvl((select max(x.datariavvio) x
                         from rdl x
                        where x.apparato = t.apparato
                          and x.impianto = t.impianto
                          and x.datariavvio <= t.datarichiesta
                          and x.datariavvio is not null),
                        v.appdatainservice),
                   4)) as mtbf
                   ,
                   (select count(y.oid) from rdl y where y.apparato = t.apparato and    y.datariavvio is not null and    y.datariavvio > y.datafermo) as conta
  from rdl t,
       
       (select a.oid oidapparato,
               a.cod_descrizione || '-' || a.descrizione as apparatodesc,
               st.descrizione || '-' || st.cod_descrizione as tipoapparato,
               i.oid as oidimpianto,
               i.descrizione || '-' || i.cod_descrizione as impiantodesc,
               e.oid as oidedificio,
               e.cod_descrizione as edificiodesc,
               c.wbs as commessa,
               c.referentecofely as oidreferentecofely,
               c.centrocosto,
               ar.descrizione as areadipolo,
               a.datainservice  appdatainservice
          from apparato    a,
               impianto    i,
               edificio    e,
               apparatostd st,
               commesse    c,
               areadipolo  ar
         where st.oid = a.stdapparato
           and i.oid = a.impianto
           and i.edificio = e.oid
           and c.oid = e.commessa
           and ar.oid = c.areadipolo) v

 where t.categoria = 4
   and t.statosmistamento = 4
   and v.oidapparato = t.apparato
   and t.datacompletamento is not null
   and t.datariavvio is not null
   and t.datafermo is not null
   and t.datafermo < t.datariavvio
   and t.datacompletamento > t.datarichiesta
   and (select max(x.datariavvio) x
          from rdl x
         where x.apparato = t.apparato
           and x.impianto = t.impianto
           and x.datariavvio <= t.datarichiesta
           and x.datariavvio is not null) is not null
   and (select max(x.datariavvio) x
          from rdl x
         where x.apparato = t.apparato
           and x.impianto = t.impianto
           and x.datariavvio <= t.datarichiesta
           and x.datariavvio is not null) >
       to_date('01/01/2015', 'dd/MM/yyyy')
--   and t.datarichiesta >= datein
 order by t.apparato, t.datacompletamento
------
                
                -----
                ) loop
      
      
        insert into kpi_mtbf_fermi
          (codice,
           edificio,
           oidedificio,
           impianto,
            oidimpianto,
           apparato,
           oidapparato,
           tipoapparato,
           areadipolo,
           centrocosto,
           settimana,
           mese,
           anno,
           mtbf,
           mttr,
           mttf,
           codrdl_0,
           codrdl,
           data_riavvio_precedente,
           datafermo,
           datariavvio
           ,
           oidreferentecofely
           )        
        values
          (to_char(nr.oid),
           nr.edificiodesc,
             nr.oidedificio,
           nr.impiantodesc,
                 nr.oidimpianto,
           nr.apparatodesc,
                 nr.oidapparato,
           nr.tipoapparato,
           nr.areadipolo,
           nr.centrocosto,
           to_number(to_char(nr.datarichiesta, 'IW')),
           to_number(to_char(nr.datarichiesta, 'MM')),
           to_number(to_char(nr.datarichiesta, 'yyyy')),
           nr.mtbf,
           nr.mttr,
           nr.mttf,
           nr.oid,
           v_codrdlprec,
           nr.datariavvioprec,
           nr.datafermo,
           nr.datariavvio,
           nr.oidreferentecofely);
        commit;
  
    
    end loop;
    --p_msg_out := varmsgins;
  EXCEPTION
    WHEN OTHERS THEN
      v_err_code := SQLCODE;
      v_err_msg  := SUBSTR(SQLERRM, 1, 200);
      varmsgins  := v_err_code || '-' || v_err_msg;
    
  end Job_updkpimtbf_fermi;

  procedure Lancioupdkpimtbf(datein    date,
                             dateout   date,
                             p_msg_out out VARCHAR2) is
    varmsgins  varchar2(300) := 'lancio update kpi_mtbf ';
    v_err_code varchar2(200);
    v_err_msg  varchar2(250);
  begin
    for nr in (select oid
                 from regrdl t
                where t.data_creazione_rdl >= datein
                  and t.oid in (select r.regrdl
                                  from regsmistamentodett r
                                 where r.statosmistamento = 4)
                  and t.categoria = 4
                order by t.data_creazione_rdl) loop
      updkpimtbf(nr.oid, varmsgins);
    
    end loop;
    p_msg_out := varmsgins;
  EXCEPTION
    WHEN OTHERS THEN
      v_err_code := SQLCODE;
      v_err_msg  := SUBSTR(SQLERRM, 1, 200);
      varmsgins  := v_err_code || '-' || v_err_msg;
    
  end Lancioupdkpimtbf;

  procedure updkpimtbf(iOidREGRDL IN number, p_msg_out out VARCHAR2) is
    varmsgins        varchar2(300) := 'update kpi_mtbf';
    v_err_code       varchar2(200);
    v_err_msg        varchar2(250);
    v_maxdatariavvio date;
    v_datafermo      date;
    v_maxdUpTime     date; -- precedente alla DownTime
    v_DownTime       date;
  
    v_conta number := 0;
  begin
    --inserimento tabella kpi_mtbf
    for nr in (select rdl.oid as rdl,
                      rdl.DATARICHIESTA,
                      rdl.datacompletamento,
                      rdl.datariavvio,
                      rdl.datafermo,
                      round((nvl(rdl.datariavvio, rdl.datacompletamento) -
                            nvl(rdl.datafermo, rdl.DATARICHIESTA)) * 24 * 60) as tempomttr,
                      case
                        when (rdl.datariavvio is not null And
                             rdl.datafermo is not null) then
                         0 -- Disservizio(0) 
                        else
                         1 -- InServizio(1)
                      end as TIPOMTBF,
                      rdl.datacreazione,
                      rdl.apparato,
                      rdl.categoria,
                      stc.sistema,
                      rdl.impianto,
                      rdl.edificio,
                      co.oid as centrooperativo,
                      c.oid as commesse,
                      c.areadipolo,
                      ap.polo
                 from regrdl,
                      rdl,
                      apparato          a,
                      apparatostd       st,
                      apparatostdclassi stc,
                      impianto          i,
                      edificio          e,
                      areadipolo        ap,
                      commesse          c,
                      centrooperativo   co
                where regrdl.oid = iOidREGRDL
                  and regrdl.oid = rdl.regrdl
                  and regrdl.oid = rdl.regrdl
                  and rdl.apparato is not null
                  and rdl.apparato = a.oid
                  and a.stdapparato = st.oid
                  and st.apparatostdclassi = stc.oid
                  and a.impianto = i.oid
                  and i.edificio = e.oid
                  and c.oid = e.commessa
                  and c.areadipolo = ap.oid
                  and e.centrooperativo = co.oid) loop
    
      if nr.tipomtbf = 1 then
        --(1=inservizio); 0 condisservizio)
        --- quindi date fermo e riavvio è nullo
        select case
                 when max(k.date_completed) is null then
                  null
                 else
                  max(k.date_completed)
               end
          into v_maxdatariavvio
          from kpi_mtbf k
         where k.apparato = nr.apparato
           and k.date_completed < nr.DATARICHIESTA
           and k.oid not in (nr.rdl);
        --------------------- inserisco le date di calcolo per tipo 1
        v_datafermo := nr.DATARICHIESTA;
      
      else
        -- --- quindi date fermo e riavvio sono valorizzate
        select case
                 when max(k.datariavvio) is null then
                  null
                 else
                  max(k.datariavvio)
               end
          into v_maxdatariavvio
          from kpi_mtbf k
         where k.apparato = nr.apparato
           and k.datariavvio < nr.datafermo
           and k.oid not in (nr.rdl);
        ---------------------  inserisco le date di calcolo per tipo 0
        v_datafermo := nr.datafermo;
      end if;
    
      select count(k.oid)
        into v_conta
        from kpi_mtbf k
       where k.rdl = nr.rdl;
    
      if (v_conta > 0) then
        -- aggiorna
        update kpi_mtbf k
           set k.datarichiesta  = nr.datarichiesta,
               k.datafermo      = nr.datafermo,
               k.datariavvio    = nr.datariavvio,
               k.date_completed = nr.datacompletamento,
               k.tipomtbf       = nr.tipomtbf,
               k.tempomttr      = nr.tempomttr,
               tempomttf        = round((v_datafermo -
                                        nvl(v_maxdatariavvio, v_datafermo)) * 24 * 60),
               tempomtbf        = nr.tempomttr +
                                  round((v_datafermo -
                                        nvl(v_maxdatariavvio, v_datafermo)) * 24 * 60)
        /*               tempomttf        = round((nr.datafermo -
        nvl(v_maxdatariavvio, nr.datafermo)) * 24 * 60),
        tempomtbf        = nr.tempomttr +
        round((nr.datafermo -
        nvl(v_maxdatariavvio, nr.datafermo)) * 24 * 60)*/
         where k.rdl = nr.rdl;
        commit;
      
      end if;
    end loop;
  
    p_msg_out := varmsgins;
  EXCEPTION
    WHEN OTHERS THEN
      v_err_code := SQLCODE;
      v_err_msg  := SUBSTR(SQLERRM, 1, 200);
      varmsgins  := v_err_code || '-' || v_err_msg;
    
  end updkpimtbf;

  procedure inskpimtbf(iOidREGRDL IN number, p_msg_out out VARCHAR2) is
    varmsgins        varchar2(300) := 'Insert into kpi_mtbf';
    v_err_code       varchar2(200);
    v_err_msg        varchar2(250);
    v_maxdatariavvio date;
    v_datafermo      date;
    v_maxdUpTime     date; -- precedente alla DownTime
    v_DownTime       date;
  
    v_conta number := 0;
  begin
    --inserimento tabella kpi_mtbf
    for nr in (select rdl.oid as rdl,
                      rdl.DATARICHIESTA,
                      rdl.datacompletamento,
                      rdl.datariavvio,
                      rdl.datafermo,
                      round((nvl(rdl.datariavvio, rdl.datacompletamento) -
                            nvl(rdl.datafermo, rdl.DATARICHIESTA)) * 24 * 60) as tempomttr,
                      case
                        when (rdl.datariavvio is not null And
                             rdl.datafermo is not null) then
                         0 -- Disservizio(0) 
                        else
                         1 -- InServizio(1)
                      end as TIPOMTBF,
                      rdl.datacreazione,
                      rdl.apparato,
                      rdl.categoria,
                      stc.sistema,
                      rdl.impianto,
                      rdl.edificio,
                      co.oid as centrooperativo,
                      c.oid as commesse,
                      c.areadipolo,
                      ap.polo
                 from regrdl,
                      rdl,
                      apparato          a,
                      apparatostd       st,
                      apparatostdclassi stc,
                      impianto          i,
                      edificio          e,
                      areadipolo        ap,
                      commesse          c,
                      centrooperativo   co
                where regrdl.oid = iOidREGRDL
                  and regrdl.oid = rdl.regrdl
                  and regrdl.oid = rdl.regrdl
                  and rdl.apparato is not null
                  and rdl.apparato = a.oid
                  and a.stdapparato = st.oid
                  and st.apparatostdclassi = stc.oid
                  and a.impianto = i.oid
                  and i.edificio = e.oid
                  and c.oid = e.commessa
                  and c.areadipolo = ap.oid
                  and e.centrooperativo = co.oid) loop
      ---if da verificare in totale non ha senso 
      if nr.tipomtbf = 1 then
        --(1=inservizio); 0 condisservizio)
        --- quindi date fermo e riavvio è nullo
        select case
                 when max(k.date_completed) is null then
                  null
                 else
                  max(k.date_completed)
               end
          into v_maxdatariavvio
          from kpi_mtbf k
         where k.apparato = nr.apparato
           and k.date_completed < nr.DATARICHIESTA
           and k.oid not in (nr.rdl)
        
        ;
        --------------------- inserisco le date di calcolo per tipo 1
        v_datafermo := nr.DATARICHIESTA;
      
      else
        -- --- quindi date fermo e riavvio sono valorizzate
        select case
                 when max(k.datariavvio) is null then
                  null
                 else
                  max(k.datariavvio)
               end
          into v_maxdatariavvio
          from kpi_mtbf k
         where k.apparato = nr.apparato
           and k.datariavvio < nr.datafermo
           and k.oid not in (nr.rdl);
        ---------------------  inserisco le date di calcolo per tipo 0
        v_datafermo := nr.datafermo;
      
      end if;
      ---da verificare in totale    
    
      select count(k.oid)
        into v_conta
        from kpi_mtbf k
       where k.rdl = nr.rdl;
    
      if (v_conta > 0) then
        -- aggiorna
        update kpi_mtbf k
           set k.datarichiesta  = nr.datarichiesta,
               k.datafermo      = nr.datafermo,
               k.datariavvio    = nr.datariavvio,
               k.date_completed = nr.datacompletamento,
               k.tipomtbf       = nr.tipomtbf,
               k.tempomttr      = nr.tempomttr,
               tempomttf        = round((v_datafermo -
                                        nvl(v_maxdatariavvio, v_datafermo)) * 24 * 60),
               tempomtbf        = nr.tempomttr +
                                  round((v_datafermo -
                                        nvl(v_maxdatariavvio, v_datafermo)) * 24 * 60)
        /*               tempomttf        = round((nr.datafermo -
                                 nvl(v_maxdatariavvio, nr.datafermo)) * 24 * 60),
        tempomtbf        = nr.tempomttr +
                           round((nr.datafermo -
                                 nvl(v_maxdatariavvio, nr.datafermo)) * 24 * 60)*/
         where k.rdl = nr.rdl;
        commit;
      else
      
        insert into kpi_mtbf
          (oid,
           polo,
           areadipolo,
           centrooperativo,
           commessa,
           edificio,
           impianto,
           apparato,
           rdl,
           datarichiesta,
           date_completed,
           datafermo,
           datariavvio,
           tipomtbf,
           tempomttr,
           tempomttf,
           tempomtbf)
        values
          ("sq_KPI_MTBF".Nextval,
           nr.polo,
           nr.areadipolo,
           nr.centrooperativo,
           nr.commesse,
           nr.edificio,
           nr.impianto,
           nr.apparato,
           nr.rdl,
           nr.datarichiesta,
           nr.datacompletamento,
           nr.datafermo,
           nr.datariavvio,
           nr.tipomtbf,
           nr.tempomttr,
           round((v_datafermo - nvl(v_maxdatariavvio, v_datafermo)) * 24 * 60),
           nr.tempomttr +
           round((v_datafermo - nvl(v_maxdatariavvio, v_datafermo)) * 24 * 60));
      
        commit;
      end if;
    end loop;
    p_msg_out := varmsgins;
  EXCEPTION
    WHEN OTHERS THEN
      v_err_code := SQLCODE;
      v_err_msg  := SUBSTR(SQLERRM, 1, 200);
      varmsgins  := v_err_code || '-' || v_err_msg;
    
  end inskpimtbf;

  PROCEDURE InsertToTabellaSQL(sSQL IN varchar2, pMessaggio out VARCHAR2) is
  
    Message VARCHAR2(4000);
  begin
  
    insert into tbl_sql
      (s_sql, data)
    values
      (sSQL, systimestamp)
    RETURNING s_sql INTO Message;
  
    commit;
    pMessaggio := Message;
  end InsertToTabellaSQL;
end PK_AGGIORNA_KPI;

/*  procedure updkpimtbf(iOidREGRDL IN number, p_msg_out out VARCHAR2) is
    varmsgins        varchar2(300) := 'update kpi_mtbf';
    v_err_code       varchar2(200);
    v_err_msg        varchar2(250);
    v_maxdatariavvio date;
    v_datafermo      date;
    v_conta          number := 0;
  begin
    --inserimento tabella kpi_mtbf
    for nr in (select rdl.oid as rdl,
                      rdl.DATARICHIESTA,
                      rdl.datacompletamento,
                      ---case when da implementare--
                      rdl.datariavvio,
                      rdl.datafermo,
                      round((nvl(rdl.datariavvio, rdl.DATARICHIESTA) -
                            nvl(rdl.datafermo, rdl.datacompletamento)) * 24 * 60) as tempomttr,
                      case
                        when (nvl(rdl.datariavvio, rdl.datacreazione) -
                             nvl(rdl.datafermo, rdl.datacreazione)) > 0 then
                         1
                        else
                         0
                      end as TIPOMTBF,
                      rdl.datacreazione,
                      rdl.apparato,
                      rdl.categoria
               
                 from regrdl, rdl, apparato a
                where regrdl = iOidREGRDL
                  and regrdl.oid = rdl.regrdl
                  and rdl.apparato is not null
                  and rdl.datacompletamento is not null
                  and rdl.apparato = a.oid
                order by rdl.apparato,
                         rdl.datariavvio,
                         rdl.datacompletamento) loop
      if nr.tipomtbf = 0 then
        -- senza disservizio  #########
        select case
                 when max(k.date_completed) is null then
                  null
                 else
                  max(k.date_completed)
               end
          into v_maxdatariavvio
          from kpi_mtbf k
         where k.apparato = nr.apparato
           and k.oid not in (nr.rdl);
        --------------------- inserisco le date di calcolo per tipo 0
        v_datafermo := nr.DATARICHIESTA;
      
      else
        --- con disservizio                        ####
        select case
                 when max(k.datariavvio) is null then
                  null
                 else
                  max(k.datariavvio)
               end
          into v_maxdatariavvio
          from kpi_mtbf k
         where k.apparato = nr.apparato
           and k.oid not in (nr.rdl);
        ---------------------  inserisco le date di calcolo per tipo 1
        v_datafermo := nr.datafermo;
      
      end if;
    
      select count(k.oid)
        into v_conta
        from kpi_mtbf k
       where k.rdl = nr.rdl;
    
      if (v_conta > 0) then
        -- aggiorna
        update kpi_mtbf k
           set k.datafermo      = nr.datafermo,
               k.datariavvio    = nr.datariavvio,
               k.date_completed = nr.datacompletamento,
               k.tipomtbf       = nr.tipomtbf,
               k.tempomttr      = nr.tempomttr,
               tempomttf        = round((v_datafermo -
                                        nvl(v_maxdatariavvio, v_datafermo)) * 24 * 60),
               tempomtbf        = nr.tempomttr +
                                  round((v_datafermo -
                                        nvl(v_maxdatariavvio, v_datafermo)) * 24 * 60)
        \*               tempomttf        = round((nr.datafermo -
                                 nvl(v_maxdatariavvio, nr.datafermo)) * 24 * 60),
        tempomtbf        = nr.tempomttr +
                           round((nr.datafermo -
                                 nvl(v_maxdatariavvio, nr.datafermo)) * 24 * 60)*\
         where k.rdl = nr.rdl;
        commit;
      end if;
    end loop;
    p_msg_out := varmsgins;
  EXCEPTION
    WHEN OTHERS THEN
      v_err_code := SQLCODE;
      v_err_msg  := SUBSTR(SQLERRM, 1, 200);
      varmsgins  := v_err_code || '-' || v_err_msg;
    
  end updkpimtbf;









  --------
      select t.oid,
                      t.apparato   oidapparato,
                      v.apparatodesc,
                      v.oidedificio,
                       v.edificiodesc,
                      v.oidimpianto,
                         v.impiantodesc,
                      v.centrocosto,
                      v.commessa,
                      v.oidreferentecofely ,
                      v.areadipolo,
                      v.tipoapparato,
                      t.datacompletamento,
                      t.datarichiesta,
                      round(round(t.datacompletamento - t.datarichiesta, 4) * 24 * 60) as mttr
                 from rdl t,
                      
                      (select a.oid oidapparato,
                              a.cod_descrizione || '-' || a.descrizione as apparatodesc,
                              st.descrizione || '-' || st.cod_descrizione as tipoapparato,
                              i.oid as oidimpianto,
                               i.descrizione || '-' || i.cod_descrizione as impiantodesc,
                                e.oid as oidedificio,
                              e.cod_descrizione as edificiodesc,
                              c.wbs as commessa,
                                c.referentecofely as oidreferentecofely,
                              c.centrocosto,
                              ar.descrizione as areadipolo
                         from apparato    a,
                              impianto    i,
                              edificio    e,
                              apparatostd st,
                              commesse    c,
                              areadipolo  ar
                        where st.oid = a.stdapparato
                          and i.oid = a.impianto
                          and i.edificio = e.oid
                          and c.oid = e.commessa
                          and ar.oid = c.areadipolo) v
               
                where --t.data_creazione_rdl >= datein   and 
                t.regrdl in (select r.regrdl
                               from regsmistamentodett r
                              where r.statosmistamento = 4)
            and t.categoria = 4
            and t.apparato in (select t.apparato
                                 from rdl t
                                where t.categoria = 4
                                  and t.regrdl in
                                      (select r.regrdl
                                         from regsmistamentodett r
                                        where r.statosmistamento = 4)
                                group by t.apparato
                               having count(t.rowid) > 1)
            and v.oidapparato = t.apparato
            and t.datacompletamento is not null
            and t.datacompletamento > datarichiesta
         --   and t.datarichiesta >= datein
                order by t.apparato, t.datacompletamento
       ------  


















  */
/
