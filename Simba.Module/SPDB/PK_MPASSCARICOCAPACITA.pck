CREATE OR REPLACE PACKAGE PK_MPASSCARICOCAPACITA is

  --- dichiarazione dei cursori statici

  --cursor cSkAtt (pScenario number) is
  cursor cSkAtt is
    select sc.descrizione,
           sc.centrooperativo,
           c.DESCRIZIONE       clasterdesc,
           c.PRESIDIO,
           c.TMPCOE,
           c.TMPE,
           imp.descrizione     impdesc,
           imp.cod_descrizione,
           imp.edificio,
           ap.oid,
           ap.cod_descrizione  apcod,
           ap.descrizione      apdesc,
           pmp.schedempowner,
           frq.oid             frequenze,
           frq.descrizione     freq,
           pmp.mansioniopt,
           m.cod_descrizione   mansione,
           m.skildes,
           m.skillevdesc,
           mca.settimana,
           mca.anno

      from apparato ap, -- 6828  584
           apparatoschedamp apmp,
           apparatostd astd,
           FREQUENZE frq,
           schedemp pmp,
           impianto imp,
           edificio e,
           clusteredifici c,
           scenario sc,
           (select m.oid,
                   m.cod_descrizione,
                   m.skill,
                   s.descrizione     skildes,
                   m.skilllevel,
                   sl.descrizione    skillevdesc
              from mansioni m, skill s, skilllevel sl
             where m.skill = s.oid
               and m.skilllevel = sl.oid) m,
           mpmaca mca

     where pmp.oid = apmp.schedamp
       and apmp.apparato = ap.oid
       and pmp.apparatostd = astd.oid
       and pmp.frequenzaopt = frq.oid
       and pmp.mansioniopt = m.oid
       and ap.impianto = imp.oid
       and imp.edificio = e.oid
          --  and C.OID = imp."CLUSTER"
       and e.clusteredifici = c.oid
       and sc.oid = c.SCENARIO
       and mca.frequenze = frq.oid;
  ---ocSkAtt   cSkAtt%ROWTYPE;
  TYPE TcSkAtt IS REF CURSOR RETURN cSkAtt%ROWTYPE;
  TYPE T_PIANOATTIVITA IS REF CURSOR RETURN V_PIANOATTIVITA%ROWTYPE;

  ---------   cursore delle MANSIONE - APPARATI
  cursor cMansApp(pScenario number) is
    select m.oid              mansioneoid,
           m.cod_descrizione  mansionecod,
           ap.oid             appoid,
           ap.cod_descrizione appocod,
           pmp.apparatostd
      from apparato         ap, -- 6828  584
           apparatoschedamp apmp,
           apparatostd      astd,
           schedemp         pmp,
           impianto         imp,
           edificio         e,
           -- V_CLUSTER c,
           clusteredifici c,
           scenario sc,
           (select m.oid,
                   m.cod_descrizione,
                   m.skill,
                   s.descrizione     skildes,
                   m.skilllevel,
                   sl.descrizione    skillevdesc
              from mansioni m, skill s, skilllevel sl
             where m.skill = s.oid
               and m.skilllevel = sl.oid) m
     where pmp.oid = apmp.schedamp
       and apmp.apparato = ap.oid
       and pmp.apparatostd = astd.oid
       and pmp.mansioniopt = m.oid
       and ap.impianto = imp.oid
          -- and C.OID = imp."CLUSTER"
       and C.OID = e.clusteredifici
       and e.oid = imp.edificio
       and sc.oid = c.SCENARIO
       and sc.oid = pScenario --1
     group by ap.oid,
              ap.cod_descrizione,
              c.OID,
              imp.cod_descrizione,
              ap.stdapparato,
              ap.cod_descrizione,
              m.cod_descrizione,
              pmp.apparatostd,
              m.oid
     order by m.cod_descrizione,
              c.OID,
              imp.cod_descrizione,
              ap.stdapparato,
              ap.cod_descrizione;

  TYPE TcMansApp IS REF CURSOR RETURN cMansApp%ROWTYPE;

  --------------------------------------------------------------
  -- cursore risorse team per associare ai ghost
  ---------   cursore delle MANSIONE - APPARATI
  cursor cTeamGhostM(Anno     number,
                     CO       centrooperativo.oid%type,
                     Mansione mansioni.oid%type,
                     CLINK    number) is
    select distinct y.risorsateamoid
      from (select x.anno || '-' || r. centrooperativo || '-' || r.mansione as codice,
                   r.oid oidrisorsa,
                   r.nome,
                   r.cognome,
                   r.mansione,
                   r.centrooperativo,
                   (select count(a.oid)
                      from ASSRISORSETEAM a
                     where a.risorseteam = x.risorsateamoid) clink,
                   'tr' as tr,
                   x.risorsateamoid,
                   x.risorsacapo,
                   x.mpghost,
                   x.anno,
                   x.risorsa,
                   x.oidassrisorse
              from RISORSE r,
                   (select rt.oid         risorsateamoid,
                           rt.risorsacapo,
                           rt.mpghost,
                           rt.anno,
                           rta.risorsa,
                           rta.oid        oidassRisorse
                      from RISORSETEAM rt, ASSRISORSETEAM rta
                     where rt.oid = rta.risorseteam) x
             where r.oid = x.risorsacapo) y
     where y.codice = Anno || '-' || CO || '-' || Mansione
       and y.anno = Anno -- 2014
       and y.centrooperativo = CO -- 16
       and y.mansione = Mansione - 51 --42
       and y.clink = CLINK --1
       and mpghost is null;

  TYPE TcTeamGhostM IS REF CURSOR RETURN cTeamGhostM%ROWTYPE;
  -------------------------
  -- cursore risorse team per associare ai ghost
  ---------   cursore delle MANSIONE - APPARATI
  cursor cTeamGhostS(Anno  number,
                     CO    number,
                     Skill skill.oid%type,
                     CLINK number) is
    select distinct y.risorsateamoid
      from (select r.oid oidrisorsa,
                   r.nome,
                   r.cognome,
                   r.mansione,
                   m.skill,
                   r. centrooperativo,
                   (select count(a.oid)
                      from ASSRISORSETEAM a
                     where a.risorseteam = x.risorsateamoid) clink,
                   'tr' as tr,
                   x.risorsateamoid,
                   x.risorsacapo,
                   x.mpghost,
                   x.anno,
                   x.risorsa,
                   x.oidassrisorse
              from RISORSE r,
                   mansioni m,
                   (select rt.oid         risorsateamoid,
                           rt.risorsacapo,
                           rt.mpghost,
                           rt.anno,
                           rta.risorsa,
                           rta.oid        oidassRisorse
                      from RISORSETEAM rt, ASSRISORSETEAM rta
                     where rt.oid = rta.risorseteam) x

             where r.mansione = m.oid
               and r.oid = x.risorsacapo) y
     where y.anno = Anno -- 2014
       and y.centrooperativo = CO -- 16
       and y.skill = Skill --42
       and y.clink = CLINK --1
       and mpghost is null;

  TYPE TcTeamGhostS IS REF CURSOR RETURN cTeamGhostS%ROWTYPE;
  -------------------------
    -------------------------
  -- cursore risorse team per associare ai ghost
  ---------   cursore delle Risorese Team per edificio calcolate per le RdL
  cursor cTRisorseEdificioforRdL(OidEdificio  Edificio.oid%type
                    ) is
  ---create or replace view v_risorseteam_calc_base as
select co.descrizione as CentroOperativo,
       co.oid as OidCentroOperativo,
       ri.oid as OidRisorsaTeam,
       regrdlultimo.statooperativo as ultimostatooperativo,
       t.nome || ' ' || t.cognome as RisorsaCapoSquadra,
       m.descrizione as Mansione,
       case
         when regrdlag.nrattagenda is null then
          0
         else
          regrdlag.nrattagenda
       end as nrattagenda,
       case
         when regrdlsos.nrattsospese is null then
          0
         else
          regrdlsos.nrattsospese
       end as nrattsospese,
       regrdlemerg.nrattemergenza as nrattemergenza,
       t.telefono,
       regrdlultimo.descrizione as regrdlassociato, --descrizione ultimo registro associato

       (select count(cr.oid)
          from conduttori cr
         where cr.edificio = OidEdificio  -------------------------------########################
         --cr.edificio = distedificio.edificio
           and cr.risorsateam = ri.oid
           ) as Conduttore, --edificio filtrato della regrdl
       ar.nr_risorse as numman, --numrisorse ssociate al team
       u."UserName" as username, --nomerisolto
       distedificio.DISTANZA as distanzaimpianto,
       distedificio.EDIFICIO as edificioregrdl,
       regrdlultimo.cod_edificio as ultimoedificio,
       (select 'nr interventi per edificio ' || count(rdl.oid)
          from rdl
         where rdl.edificio = OidEdificio  -------------------------------########################
         -- rdl.edificio = distedificio.edificio
         ) as interventisuedificio
         ----------------  ag  -------------------
        , distedificio.edificio as oidedificio
-- 'nr interventi per edificio ' as interventisuedificio
--, so.statooperativo as ultimostatooperativo

  from risorseteam ri,
       RISORSE t,
       (select u."Oid", u."UserName" from "SecuritySystemUser" u) u,
       centrooperativo co,
       mansioni m --, statooperativo so
       ,
       (select risorsateam, count(regrdl.rowid) as nrattagenda
          from regrdl
         where regrdl.ultimostatosmistamento in (2)
           and regrdl.ultimostatooperativo = 19
           and regrdl.risorsateam is not null
         group by regrdl.risorsateam) regrdlag,

       (select risorsateam, count(regrdl.rowid) as nrattsospese
          from regrdl
         where regrdl.ultimostatosmistamento in (3)
           and regrdl.ultimostatooperativo in (6, 7, 8, 9, 10)
           and regrdl.risorsateam is not null
         group by regrdl.risorsateam) regrdlsos,

       (select risorsateam, count(regrdl.rowid) as nrattemergenza
          from regrdl
         where regrdl.ultimostatosmistamento in (10)
           and regrdl.risorsateam is not null
         group by regrdl.risorsateam) regrdlemerg,

       (select k.risorsateam,
               regrdl.oid,
               regrdl.descrizione,
               e.oid,
               e.cod_descrizione as cod_edificio,
               so.statooperativo
          from regrdl,
               edificio e,
               impianto i,
               apparato a,
               statooperativo so,
               (select v.risorsateam, v.maxdata, max(regrdl.oid) as maxregrdl
                --so.statooperativo,
                --regrdl.oid, rdl.edificio,rdl.impianto
                  from regrdl,
                       regoperativodettaglio rod --,rdl
                       --,statooperativo so
                      ,
                       (select regrdl.risorsateam, max(t.dataora) as maxdata
                          from regoperativodettaglio t, regrdl
                         where regrdl.oid = t.regrdl
                         group by regrdl.risorsateam) v
                 where v.maxdata = rod.dataora
                   and rod.regrdl = regrdl.oid
                 group by v.risorsateam, v.maxdata) k
         where k.maxregrdl = regrdl.oid
           and regrdl.apparato = a.oid
           and a.impianto = i.oid
          -- and i.edificio = e.oid
           -------------------
           and i.edificio = OidEdificio   ------2342-------------------------########################
           and  e.oid   = OidEdificio   -------2342------------------------########################
           ---------------
           and so.oid = regrdl.ultimostatooperativo) regrdlultimo,
       (select ar.risorseteam, count(ar.rowid) as nr_risorse
          from assrisorseteam ar
         group by ar.risorseteam) ar,
       (select kk.edificio,
               kk.RISORSACAPO,
               kk.centrooperativo,
               CASE
                 WHEN DISTANZA = 0 then
                  'Non Calcolabile '
                 WHEN DISTANZA > 0 and DISTANZA <= 25 THEN
                  'in Prossimità'
                 WHEN DISTANZA > 25 and DISTANZA <= 50 THEN
                  'Vicino'
                 WHEN DISTANZA > 50 THEN
                  'Lontano'
                 ELSE
                  'Non Calcolabile'
               END as DISTANZA

          from (select RISORSACAPO,
                       EDIFICIO,
                       centrooperativo,
                       --INDIRIZZO,
                       acos(round(sin(round(longitudine_edificio, 5)) *
                                  sin(round(longitudine_risorsa, 5)) +
                                  Cos(round(longitudine_edificio, 5)) *
                                  cos(round(longitudine_risorsa, 5)) *
                                  cos(round(latitudine_risorsa, 5) -
                                      round(latitudine_edificio, 5)),
                                  5)) * 6371 DISTANZA
                  from (select nvl(i.geolat, 41.904321) latitudine_edificio,
                               nvl(i.geolng, 12.491455) longitudine_edificio,
                               nvl(r.geolat, 41.904321) latitudine_risorsa,
                               nvl(r.geolng, 12.491455) longitudine_risorsa,
                               --t.Oid RISORSETEAM,
                               e.oid       EDIFICIO,
                               i.oid       INDIRIZZO,
                               c.oid       as centrooperativo,
                               risorsacapo RISORSACAPO
                          from edificio        e,
                               indirizzo       i,
                               risorse         r,
                               risorseteam     t,
                               centrooperativo c,
                               commesse        cm
                         where e.indirizzo = i.oid
                           and e.commessa = cm.oid
                           and r.oid = t.risorsacapo
                           and r.centrooperativo = c.oid
                           and c.areadipolo=cm.areadipolo
                           and e.cod_descrizione not in ('093','PROVA_IARC','PROVA_IARO','LOCALITA-CRV-EDILE','PROVA_IARL','38004-IARO')
                           and t.risorsacapo not in
                           (
                           502,522,523,602,622,623,702,722,723
                           )
                           -----------------
                           and e.oid = OidEdificio   -----------2342--------------------########################
                           ----------------
                        --and r.securityuserid is not null
                        ) vv) kk) distedificio

 where co.oid = t.centrooperativo
      --and co.cod_descrizione='IARCC'
   and ri.risorsacapo = t.oid
   and m.oid = t.mansione
   and ri.oid = regrdlag.risorsateam(+)
   and ri.oid = regrdlsos.risorsateam(+)
   and ri.oid = regrdlemerg.risorsateam(+)
   and ri.oid = regrdlultimo.risorsateam(+)
   and t.securityuserid = u."Oid"(+)
   and ar.risorseteam = ri.oid
   and t.oid || t.centrooperativo =
       distedificio.RISORSACAPO || distedificio.centrooperativo
   and distedificio.EDIFICIO   = OidEdificio  ---------2342----------------------########################

--and so.oid(+)=ri.ultimostatooperativo
;


  TYPE TcTRisorseEdificioforRdL IS REF CURSOR RETURN cTRisorseEdificioforRdL%ROWTYPE;
  -------------------------
  ---------------------------------------------------
  /*TABCAL TIPOCALCOLO := TIPOCALCOLO;*/
  /*CREATE TABLE atable (
  col1 varchar2(10),
  CONSTRAINT cons_atable_col1 CHECK (col1 IN ('Monday', 'Tuesday', 'Wednesday', 'Thursday',
  'Friday', 'Saturday', 'Sunday'))
  );*/

  TYPE enumTableCalc IS RECORD(
    Ghost    number := 1,
    Ghosts   number := 2,
    Risorse  number := 3,
    TRisorse number := 4);
  eTabCalc enumTableCalc;

  TYPE enumSaturazioniValori IS RECORD(
    Media   number := 0,
    Minimo  number := 0,
    Massimo number := 0,
    Mediana number := 0,
    Moda    number := 0);
  eSaturaVal enumSaturazioniValori;
  --x := g_color.yellow;
  --e1 ENUM('a','b','c');

  subtype colors is binary_integer range 1 .. 4;
  ---------------------
  /*  CREATE TYPE SettimaneList AS TABLE OF VARCHAR2(10) -- define type
  CREATE TYPE Student AS OBJECT( -- create object
                                id_num INTEGER(4),
                                Valore number) -- declare nested table as attribute

  TYPE BOOLlo IS NEW BOOLEAN;*/
  ---------------------------------------------------------------
  TYPE t_arraySql IS TABLE OF VARCHAR2(4000);
  TYPE T_CURSOR IS REF CURSOR;
  log_id number default 0;

  -----------

  PROCEDURE GetRisorsedaAssociare(iRegPiano  IN number,
                                  iUtente    IN varchar2,
                                  oMessaggio out VARCHAR2,
                                  oCriteria  out VARCHAR2);

  ----        gettrisorsedaassociaremansione
  PROCEDURE GetTRisorsedaAssociareManSki(iGhost     IN number,
                                         oMessaggio out VARCHAR2,
                                         IO_CURSOR  IN OUT T_CURSOR);

  PROCEDURE GetTRisorsedaAssocManSki_old(iGhost             IN number,
                                         iCoppiaLinkata     IN number,
                                         iTAssoGostTRisorse IN number,
                                         iUtente            IN varchar2,
                                         oCriteria          out VARCHAR2,
                                         oMessaggio         out VARCHAR2);

  PROCEDURE GetTRisorsedaAssMansioneT(risorsa      IN number,
                                      datavalidita IN date,
                                      --oCriteria  out VARCHAR2,
                                      oMessaggio out VARCHAR2);

  PROCEDURE CreaRisorsaTeam(iOidRisorsa IN number,
                            iAnno       IN varchar2,
                            iUtente     IN varchar2,
                            oMessaggio  out VARCHAR2);

  PROCEDURE RilasciaRisorsedaTeam(iOidRisorsaTeam IN number,
                                  iUtente         IN varchar2,
                                  oMessaggio      out VARCHAR2);

  PROCEDURE CreaCoppiaLinkata(iOidRisorsa IN number,
                              iUtente     IN varchar2,
                              oMessaggio  out VARCHAR2,
                              oCriteria   out VARCHAR2);

  PROCEDURE CreaCoppiaLinkataConRisorsa(iOidRisorsaTeam IN number,
                                        iOidRisorsa     IN number,
                                        iUtente         IN varchar2,
                                        oMessaggio      out VARCHAR2);

  PROCEDURE RisorsaCaricaComboCoppiaLink(iOidRisorsa IN number,
                                         iUtente     IN varchar2,
                                         oMessaggio  out VARCHAR2,
                                         IO_CURSOR   IN OUT T_CURSOR);

  PROCEDURE RisorsaTeamCaricaComboCLink(iOidRisorsaTeam IN number,
                                        iUtente         IN varchar2,
                                        oMessaggio      out VARCHAR2,
                                        IO_CURSOR       IN OUT T_CURSOR);

  PROCEDURE AssociazioneCaricoCapacita(iOidRisorsaTeam IN number,
                                       iOIdGhost       IN number,
                                       iUtente         IN varchar2,
                                       oMessaggio      out VARCHAR2);

  PROCEDURE GetRisorsexTask(iOidRdL IN VARCHAR2,
                            --iUtente   IN varchar2,
                            IO_CURSOR IN OUT T_CURSOR);

  PROCEDURE GetDistanzeRisorseTeam(iOidRisorsaTeam   IN number,
                                   iOidImpianto      IN number,
                                   oDistanza         out VARCHAR2,
                                   oIncaricoImpianto out VARCHAR2,
                                   oUltimoImpianto   out VARCHAR2,
                                   oRdLSospese       out varchar2,
                                   oRdLAssegnate     out varchar2,
                                   oMessaggio        out VARCHAR2);

  PROCEDURE NrInterventiInEdificio(iOidRisorsaTeam IN number,
                                   iOidEdificio    IN number,
                                   oNrInterventi   out VARCHAR2,
                                   oMessaggio      out VARCHAR2);

   PROCEDURE AggiornaRdLbySSmistamento(iOidRegRdl in number,
                                      iUtente in varchar2,
                                      iTipo      IN varchar2,
                                      oMessaggio out varchar2);

 PROCEDURE CreaRegRdLbyRdL
    (iOidRdl in number,iutente in varchar2, oMessaggio out varchar2);

  PROCEDURE RdlSospesa(iOidRdl IN number, oMessaggio out VARCHAR2);

  PROCEDURE RdlAnnullata(iOidRdl IN number, oMessaggio out VARCHAR2);
  PROCEDURE RegrdlAnnullata(iOidRegRdl IN number, oMessaggio out VARCHAR2);

  PROCEDURE RegRdLEmergenzadaCO(iOidRegRdl IN number,
                                iTipo      IN varchar2,
                                oMessaggio out VARCHAR2);

  PROCEDURE RdlCambioRisorsaTeam(iOidRdl         IN number,
                                 iOidRisorsaTeam in number,
                                 oMessaggio      out VARCHAR2);
  PROCEDURE RegrdlCambioRisorsaTeam(iOidRegRdl      IN number,
                                    iOidRisorsaTeam in number,
                                    oMessaggio      out VARCHAR2);

  PROCEDURE RegRdlAssegnaRisorsaTeam(iOidRegRdl      IN number,
                                     iOidRisorsaTeam in number,
                                     oMessaggio      out VARCHAR2);

  PROCEDURE RdlMigrazionepmptt(iOidRdl IN number, oMessaggio out VARCHAR2);
  PROCEDURE RegrdlMigrazionepmpt(iOidRegRdl IN number,
                                 ouser      in varchar2,
                                 oMessaggio out VARCHAR2);
  PROCEDURE RdlCompletamento(iOidRegRdl IN number, oMessaggio out VARCHAR2);
  PROCEDURE RegrdlCompletamento(iOidRegRdl IN number,
                                oMessaggio out VARCHAR2);
  PROCEDURE Parlinkcompletamento(oMessaggio out VARCHAR2);

  procedure inskpimtbf(iOidREGRDL IN number, p_msg_out out VARCHAR2);
  PROCEDURE UPDDELTATREGSMPREC(iOidREGSMDET IN number,
                               odate        in date,
                               oMessaggio   out VARCHAR2);

  PROCEDURE UPDDELTATREGSOPREC(iOidREGSODET IN number,
                               odate        in date,
                               oMessaggio   out VARCHAR2);

  FUNCTION GetDistanzeCOImpianto(iOidCluster  IN number,
                                 iOidEdificio IN number,
                                 iOidImpianto IN number /*,
                                                                                                                                                                                                                                                                                                                                            oDistanza out VARCHAR2,
                                                                                                                                                                                                                                                                                                                                            oMessaggio out VARCHAR2*/
                                 --IO_CURSOR   IN OUT T_CURSOR
                                 ) Return VARCHAR2;
  FUNCTION GetDistanzeCOEdificio(iOidCluster  IN number,
                                 iOidEdificio IN number
                                 --IO_CURSOR   IN OUT T_CURSOR
                                 ) Return VARCHAR2;
  FUNCTION GetDescCambioSOperativo(v_stop_pre   IN number,
                                   v_SOperativo IN number) Return VARCHAR2;

    procedure RISORSE_EDIFICIO_RDL
    (iOidEdificio in number,
    iusername in varchar2,
    oMessaggio out varchar2,
    IO_CURSOR  IN OUT T_CURSOR);
 procedure clona_regrdl_mp (iregrdl in number, omessage out varchar2);

procedure UPDATETEAMCOUNT
  (irisorseteam in number,
  omessage out varchar2 );
procedure lancioaggrdlbysmistamento(outnum out number);
end PK_MPASSCARICOCAPACITA;
/
create or replace package body PK_MPASSCARICOCAPACITA is
  -- Schedulazione Manutenzione programmata Settimanale

  ------- @@@@@@@@@@@@@@@  piani annuali @@@@@@@@@@@@@@@@@@@@@@@@@@

  PROCEDURE GetRisorsedaAssociare(iRegPiano  IN number,
                                  iUtente    IN varchar2,
                                  oMessaggio out VARCHAR2,
                                  oCriteria  out VARCHAR2)

   IS
    cursor c1 is
      select --t.*, t.rowid
       R.OID,
       R.NOME,
       R.COGNOME,
       R.MATRICOLA,
       R.MANSIONE,
       R.CENTROOPERATIVO,
       Rt.oid risorsateam,
       RT.RISORSACAPO,
       RT.MIN,
       RT.MAX,
       RT.MEDIA,
       RT.MODA,
       RT.MEDIANA
      --R.*
        from RISORSE             R,
             risorseteam         RT,
             assrisorseteam      art,
             centrooperativo     co,
             mpregpianificazione rg,
             scenario            s
       WHERE aRt.RISORSETEAM = RT.OID
         and art.risorsa = r.oid
         AND RT.MPGHOST IS NULL
         AND R.CENTROOPERATIVO = s.centrooperativo
         and rg.scenario = s.oid
         and rg.oid = iRegPiano
      --and rg.oid = 2
       group by R.OID,
                R.NOME,
                R.COGNOME,
                R.MATRICOLA,
                R.MANSIONE,
                R.CENTROOPERATIVO,
                Rt.oid,
                RT.RISORSACAPO,
                RT.MIN,
                RT.MAX,
                RT.MEDIA,
                RT.MODA,
                RT.MEDIANA;

    /*    ContaNome        NUMBER;
    pPercorsoMP      varchar2(1000);
    pRevisione       varchar2(3);
    pSEDE            varchar2(10);
    pSorgenteFilePAM varchar2(1000);
    pOutFNomeXls     varchar2(1000);*/

    sett     varchar2(1000);
    settcorr varchar2(1000);
  BEGIN

    /* ------------------------------------------------------------------
    OPEN V_CURSOR FOR

    IO_CURSOR := V_CURSOR;*/
    ----------  gestione errore --------------------
    for r_c1 in c1 loop
      settcorr := r_c1.OID || ',';
      sett     := sett || settcorr;
    END LOOP;
    oCriteria := Substr(sett, 1, Length(sett) - 1);

    ----------------------------------------
    /*  EXCEPTION
    WHEN BLVOUTO THEN
      p_msgout := 'errore edificio non presente';
    WHEN RESPOTECNVUOTO THEN
      p_msgout := 'errore nel recupreo del responsabile tecnico';
    WHEN OTHERS THEN
      p_msgout := 'errore Sconoscioto';*/

  END GetRisorsedaAssociare;

  ----        gettrisorsedaassociaremansione
  PROCEDURE GetTRisorsedaAssociareManSki(iGhost     IN number,
                                         oMessaggio out VARCHAR2,
                                         IO_CURSOR  IN OUT T_CURSOR) IS

    v_anno         number := 0;
    v_scenario     scenario.oid%type;
    v_co           centrooperativo.oid%type;
    v_mansione     mansioni.oid%type;
    v_numman       number := 0;
    v_skill        skill.oid%type;
    v_min_data     date;
    v_max_data     date;
    v_normacogente number := 0;

    V_CURSOR T_CURSOR;
  BEGIN

 /* insert into  tbl_sql (tbl_sql.s_sql,tbl_sql.data )
  values ('GetTRisorsedaAssociareManSki input iGhost: '||iGhost
  ,sysdate); commit;*/


        select
        t.centrooperativo,
        t.mansione,
        t.numman,
        MIN(T.DATAFISSA),
        MAX(T.DATAFISSA),
        max(ask.normacogente),
        m.skill
        into v_co,
        v_mansione,
        v_numman,
        v_min_data,
        v_max_data,
        v_normacogente,
        v_skill
        from MPATTIVITAPIANIFICATEDETT t,
        MPATTIVITAPIANIFICATE     ap,
        apparatoschedamp          ask,
        mansioni                  m
        where ap.apparatoschedamp = ask.oid
        and t.mpattivitapianificate = ap.oid
        and ask.mansioniopt = m.oid
        and t.mpghost = iGhost --3406
        GROUP BY t.centrooperativo, t.mansione, t.numman, m.skill;

    ----
    --dati del ghost in input
    --------------------
    /*v_normacogente = 0 puo avere 2 valori:
    -    0: restituisce teamrisorse con lo stesso skill (senza contare il livello)
    -    1: restituisce le team risorse con la stessa mansione
    icoppialinkata =  puo avere 2 valorei:
    -    0: restituisce le teamrisorse che sono coppia linkata
    -    1: restituisce teamrisorse che non sono coppia linkata*\*/
    ------------------------------------

    if v_normacogente = 0 then
     --normacogente = 0 --tutte le mansioni vanno bene
      OPEN V_CURSOR FOR
        select
         x.risorsateamoid
          from RISORSE r,
               mansioni m,
               (select rt.oid risorsateamoid,
                       rt.risorsacapo,
                       rt.mpghost,
                       rt.anno--,
                       --rta.risorsa,
                       --rta.oid     oidassRisorse

                  from RISORSETEAM rt--, ASSRISORSETEAM rta
                 where rt.mpghost is null
                 /*rt.oid = rta.risorseteam
                   AND RTA.DATAINIZIO >= v_min_data -----'18/01/2016'
                   AND RTA.DATAFINE <= v_max_data*/


                   ) x -----'18/01/2016'
         where r.oid = x.risorsacapo
           and m.oid = r.mansione
              -- and m.oid = v_mansione --9
           and (select count(a.oid)
                  from ASSRISORSETEAM a
                 where a.risorseteam = x.risorsateamoid) = v_numman --1
           and m.skill = v_skill --3
           and r.centrooperativo = v_co --119
        ;

    else
      --normacogente = 1 --mansione esatta
      OPEN V_CURSOR FOR
        select
         x.risorsateamoid
          from RISORSE r,
               mansioni m,
               (select rt.oid risorsateamoid,
                       rt.risorsacapo,
                       rt.mpghost,
                       rt.anno--,
                       --rta.risorsa,
                       --rta.oid     oidassRisorse

                  from RISORSETEAM rt--, ASSRISORSETEAM rta
                 where
                 rt.mpghost is null
                 /*rt.oid = rta.risorseteam
                   AND RTA.DATAINIZIO >= v_min_data -----'18/01/2016'
                   AND RTA.DATAFINE <= v_max_data*/

                   ) x -----'18/01/2016'
         where r.oid = x.risorsacapo
           and m.oid = r.mansione
           and m.oid = v_mansione --9
           and (select count(a.oid)
                  from ASSRISORSETEAM a
                 where a.risorseteam = x.risorsateamoid) = v_numman --1
           and m.skill = v_skill --3
           and r.centrooperativo = v_co --119
        ;
    end if;


  /*  OPEN V_CURSOR FOR
    select to_char(r.oid) as  risorsateamoid
    from risorseteam r;*/

    IO_CURSOR := V_CURSOR;
  EXCEPTION
    WHEN OTHERS THEN
      oMessaggio := 'Fallito: ' || SQLERRM;
      RAISE;

  END GetTRisorsedaAssociareManSki;

  ----        gettrisorsedaassociaremansione
  PROCEDURE GetTRisorsedaAssocManSki_old(iGhost             IN number,
                                         iCoppiaLinkata     IN number,
                                         iTAssoGostTRisorse IN number,
                                         iUtente            IN varchar2,
                                         oCriteria          out VARCHAR2,
                                         oMessaggio         out VARCHAR2) IS

    v_anno           number := 0;
    v_scenario       scenario.oid%type;
    v_co             centrooperativo.oid%type;
    v_mansione       mansioni.oid%type;
    v_coppialinkata  number := 0;
    v_MaxCarico      number := 0;
    v_skill          skill.oid%type;
    sett             varchar2(1000);
    settcorr         varchar2(1000);
    v_iCoppiaLinkata number;
  BEGIN

    --dati del ghost in input
    select nvl(p.annosk, 0) anno,
           nvl(s.oid, 0) scenario,
           nvl(s.centrooperativo, 0) centrooperativo
      into v_anno, v_scenario, v_co
      from mpregpianificazione p, scenario s
     where s.oid = p.scenario
       and p.oid in (select gh.mpregpianificazione
                       from mpghost gh
                      where gh.oid = iGhost); --143

    select x.mansione, x.coppialinkata, x.max
      into v_mansione, v_coppialinkata, v_MaxCarico
      from mpghost x
     where x.oid = iGhost;
    /*  select x.mansione
    into v_mansione ,
    from mpghost x
    where x.oid=iGhost;*/
    select distinct y.skill
      into v_skill
      from mansioni y
     where y.oid = v_mansione;

    --------------------
    /*iTAssoGostTRisorse =  puo avere 2 valori:
    -    0: restituisce le team risorse con la stessa mansione
    -    1: restituisce teamrisorse con lo stesso skill (senza contare il livello)
    icoppialinkata =  puo avere 2 valorei:
    -    0: restituisce le teamrisorse che sono coppia linkata
    -    1: restituisce teamrisorse che non sono coppia linkata*\*/
    ------------------------------------
    oCriteria := '';

    if iCoppiaLinkata = 0 then
      v_iCoppiaLinkata := 2;
    else
      v_iCoppiaLinkata := 1;
    end if;

    if iTAssoGostTRisorse = 0 then
      for rs in (select distinct y.risorsateamoid
                   from (select x.anno || '-' || r. centrooperativo || '-' || r.mansione as codice,
                                r.oid oidrisorsa,
                                r.nome,
                                r.cognome,
                                r.mansione,
                                r.centrooperativo,
                                (select count(a.oid)
                                   from ASSRISORSETEAM a
                                  where a.risorseteam = x.risorsateamoid) clink,
                                'tr' as tr,
                                x.risorsateamoid,
                                x.risorsacapo,
                                x.mpghost,
                                x.anno,
                                x.risorsa,
                                x.oidassrisorse
                           from RISORSE r,
                                (select rt.oid         risorsateamoid,
                                        rt.risorsacapo,
                                        rt.mpghost,
                                        rt.anno,
                                        rta.risorsa,
                                        rta.oid        oidassRisorse
                                   from RISORSETEAM rt, ASSRISORSETEAM rta
                                  where rt.oid = rta.risorseteam) x
                          where r.oid = x.risorsacapo) y
                  where y.anno = v_anno -- 2014
                    and y.centrooperativo = v_co -- 16
                    and y.mansione = v_mansione --51 --42
                    and y.clink = v_coppialinkata --1
                    and mpghost is null) loop

        settcorr := rs.RISORSATEAMOID || ',';
        sett     := sett || settcorr;
      end loop;

    else
      for rs in (select distinct y.risorsateamoid
                   from (select r.oid oidrisorsa,
                                r.nome,
                                r.cognome,
                                r.mansione,
                                m.skill,
                                r. centrooperativo,
                                (select count(a.oid)
                                   from ASSRISORSETEAM a
                                  where a.risorseteam = x.risorsateamoid) clink,
                                'tr' as tr,
                                x.risorsateamoid,
                                x.risorsacapo,
                                x.mpghost,
                                x.anno,
                                x.risorsa,
                                x.oidassrisorse
                           from RISORSE r,
                                mansioni m,
                                (select rt.oid         risorsateamoid,
                                        rt.risorsacapo,
                                        rt.mpghost,
                                        rt.anno,
                                        rta.risorsa,
                                        rta.oid        oidassRisorse
                                   from RISORSETEAM rt, ASSRISORSETEAM rta
                                  where rt.oid = rta.risorseteam) x

                          where r.mansione = m.oid
                            and r.oid = x.risorsacapo) y
                  where y.anno = v_anno -- 2014
                    and y.centrooperativo = v_co -- 16
                    and y.skill = v_skill --
                    and y.clink = v_coppialinkata --1
                    and mpghost is null) loop

        settcorr := rs.RISORSATEAMOID || ',';
        sett     := sett || settcorr;
      end loop;

    end if;

    /*
    if iTAssoGostTRisorse=0 then
        for rs in
          (
          select distinct v.risorseteam
          from v_team_risorse v
          where to_number(to_char(v.datamaxvalidita,'yyyy'))=v_anno
          and v.nrrisorseteam=v_iCoppiaLinkata
          and v.mansione=v_mansione
          and v.risorseteam in (select r.oid from risorseteam r where r.mpghost is null)
          )
          loop

            settcorr := rs.risorseteam || ',';
            sett     := sett || settcorr;
          end loop;
      else
          for rs in
          ( select distinct v.risorseteam
          from v_team_risorse v
          where to_number(to_char(v.datamaxvalidita,'yyyy'))=v_anno
          and v.nrrisorseteam=v_iCoppiaLinkata
          and v.skill=v_skill
          and v.risorseteam in (select r.oid from risorseteam r where r.mpghost is null)
          )
          loop

            settcorr := rs.risorseteam || ',';
            sett     := sett || settcorr;
          end loop;

      end if;

      */
    oCriteria := Substr(sett, 1, Length(sett) - 1);
  EXCEPTION
    WHEN OTHERS THEN
      oMessaggio := 'Fallito: ' || SQLERRM;
      RAISE;

  END GetTRisorsedaAssocManSki_old;

  /*
  PROCEDURE GetTRisorsedaAssociareMansione(iGhost             IN number,
                                           iCoppiaLinkata     IN number,
                                           iTAssoGostTRisorse IN number,
                                           iUtente            IN varchar2,
                                           oCriteria          out VARCHAR2,
                                           oMessaggio         out VARCHAR2)

   IS
    cursor cMansione is

      select a.oid, a.coppialinkata
        from (select
              \* r.oid,*\
               rt.oid,
               RT.RISORSACAPO,
               RT.MIN,
               RT.MAX,
               RT.MEDIA,
               RT.MODA,
               RT.MEDIANA,
               (select count(ass.oid)
                  from assrisorseteam ass
                 where \*ass.risorsa =r.oid and*\
                 ass.risorseteam = rt.oid) as coppialinkata
                from RISORSE         R,
                     risorseteam     RT,
                     centrooperativo co,
                     mpghost         g

               WHERE R.RISORSETEAM = RT.OID
                 AND RT.MPGHOST IS NULL
                 and g.mansione = r.mansione
                    \*        and ass.risorsa =r.oid
                    and ass.risorseteam= rt.oid*\
                 and g.oid = iGhost
               group by \*r.oid,*\ rt.oid,
                        RT.RISORSACAPO,
                        RT.MIN,
                        RT.MAX,
                        RT.MEDIA,
                        RT.MODA,
                        RT.MEDIANA) a
       where a.coppialinkata = iCoppiaLinkata;
    -----------------------
    cursor cSkill is
      select a.oid, a.coppialinkata
        from (select rt.oid,
                     (select count(ass.oid)
                        from assrisorseteam ass
                       where ass.risorseteam = rt.oid) as coppialinkata
                from mpghost     g,
                     mansioni    m,
                     mansioni    m_sl,
                     RISORSE     R,
                     risorseteam RT
               WHERE g.mansione = m.oid
                 and m.skill = m_sl.skill
                 and m_sl.skilllevel <= m.skilllevel
                 and m_sl.oid = r.mansione
                 and R.RISORSETEAM = RT.OID
                 and g.oid = iGhost -- 30042 --

                 and m.oid = g.mansione) a
       where a.coppialinkata = iCoppiaLinkata --1
      ;
    -------------------
    sett     varchar2(1000);
    settcorr varchar2(1000);
  BEGIN
    oCriteria := '';
    if iTAssoGostTRisorse = 0 then
      for r_c1 in cMansione loop
        settcorr := r_c1.OID || ',';
        sett     := sett || settcorr;
      END LOOP;
      oCriteria := Substr(sett, 1, Length(sett) - 1);
    else
      for r_c1 in cSkill loop
        settcorr := r_c1.OID || ',';
        sett     := sett || settcorr;
      END LOOP;
      oCriteria := Substr(sett, 1, Length(sett) - 1);
    end if;

    ----------------------------------------
    \*  EXCEPTION
    WHEN BLVOUTO THEN
      p_msgout := 'errore edificio non presente';
    WHEN RESPOTECNVUOTO THEN
      p_msgout := 'errore nel recupreo del responsabile tecnico';
    WHEN OTHERS THEN
      p_msgout := 'errore Sconoscioto';*\

  END GetTRisorsedaAssociareMansione;

  */

  PROCEDURE GetTRisorsedaAssMansioneT(risorsa      IN number,
                                      datavalidita IN date,
                                      --oCriteria  out VARCHAR2,
                                      oMessaggio out VARCHAR2)

   IS

    cursor cRisorsa is
      select vv.risorsa
        from (select t.oid as risorsa
                from risorse t
               where t.mansione in
                     (select r.mansione from risorse r where r.oid = risorsa)
                 and t.oid <> risorsa) rr,
             (select v.risorsa, v.mansione, v.nrrisorseteam
                from V_team_risorse v
               where v.nrrisorseteam = 0) vv
       where vv.risorsa = rr.risorsa
      union
      select vv.risorsa
        from (select t.oid as risorsa
                from risorse t
               where t.mansione in
                     (select r.mansione from risorse r where r.oid = risorsa)
                 and t.oid <> risorsa) rr,
             (select v.risorsa, v.mansione, v.nrrisorseteam
                from V_team_risorse v
               where v.nrrisorseteam <> 0
                 and v.datamaxvalidita < datavalidita) vv
       where vv.risorsa = rr.risorsa

      ;

    -------------------
    sett     varchar2(4000);
    settcorr varchar2(4000);
  BEGIN
    oMessaggio := '';
    sett       := '';
    settcorr   := '';

    for r_c1 in cRisorsa loop
      --if length(sett) <3998 then
      settcorr   := r_c1.risorsa || ',';
      sett       := sett || settcorr;
      oMessaggio := Substr(sett, 1, Length(sett) - 1);
      -- end if;
    END LOOP;

  END GetTRisorsedaAssMansioneT;

  PROCEDURE CreaRisorsaTeam(iOidRisorsa IN number,
                            iAnno       IN varchar2,
                            iUtente     IN varchar2,
                            oMessaggio  out VARCHAR2)

   IS
    maxoidteamriosrse number;
    Esiste            number;
  begin
    oMessaggio        := '';
    maxoidteamriosrse := 0;
    Esiste            := 0;
    --  verifica se puo' essere creata la risirsa team
    select count(*)
      into Esiste
      from risorseteam rt
     where rt.risorsacapo = iOidRisorsa -- 95
       and rt.anno = to_number(iAnno) ---2014
    ;
    -----------------------------
    if Esiste > 0 then
      oMessaggio := ' Risorsa Team già esistente!.';
      return;
    end if;

    insert into risorseteam
      (oid, risorsacapo, anno, min, max, media, moda, mediana)
    values
      ("sq_RISORSETEAM".nextval,
       iOidRisorsa,
       to_number(iAnno),
       0,
       0,
       0,
       0,
       0);
    commit;
    select max(t.oid) into maxoidteamriosrse from risorseteam t;

    insert into assrisorseteam
      (oid, risorsa, risorseteam, datainizio, datafine, tipoassociazione)
    values
      ("sq_ASSRISORSETEAM".nextval,
       iOidRisorsa,
       maxoidteamriosrse,
       to_date('01/01/' || iAnno, 'dd/mm/yyyy'),
       to_date('31/12/' || iAnno, 'dd/mm/yyyy'),
       0);
    commit;

  EXCEPTION
    WHEN OTHERS THEN
      oMessaggio := 'Fallito: ' || SQLERRM;
      RAISE;

  END CreaRisorsaTeam;

  -- elimina la risosrsa in coppia lincata
  PROCEDURE RilasciaRisorsedaTeam(iOidRisorsaTeam IN number,
                                  iUtente         IN varchar2,
                                  oMessaggio      out VARCHAR2)

   IS
  begin
    oMessaggio := '';
    delete from assrisorseteam r
     where r.risorseteam = iOidRisorsaTeam
       and r.risorsa not in
           (select rt.risorsacapo
              from risorseteam rt
             where rt.oid = iOidRisorsaTeam);
    commit;
    /*   delete
    from  risorseteam t
    where t.oid=iOidRisorsaTeam;
    commit;*/
  EXCEPTION
    WHEN OTHERS THEN
      oMessaggio := 'Fallito: ' || SQLERRM;
      RAISE;

  END RilasciaRisorsedaTeam;

  PROCEDURE CreaCoppiaLinkata(iOidRisorsa IN number,
                              iUtente     IN varchar2,
                              oMessaggio  out VARCHAR2,
                              oCriteria   out VARCHAR2) IS

    cursor cRisorsa is
      select vv.risorsa
        from (select t.oid as risorsa
                from risorse t
               where t.mansione in
                     (select r.mansione
                        from risorse r
                       where r.oid = iOidRisorsa)
                 and t.oid <> iOidRisorsa) rr,
             (select v.risorsa, v.mansione, v.nrrisorseteam
                from V_team_risorse v
               where v.nrrisorseteam = 0) vv
       where vv.risorsa = rr.risorsa;
    -------------------
    sett     varchar2(4000);
    settcorr varchar2(4000);
  BEGIN
    oMessaggio := '';
    sett       := '';
    settcorr   := '';

    for r_c1 in cRisorsa loop
      --if length(sett) <3998 then
      settcorr  := r_c1.risorsa || ',';
      sett      := sett || settcorr;
      oCriteria := Substr(sett, 1, Length(sett) - 1);
      -- end if;
    END LOOP;

  END CreaCoppiaLinkata;

  --  associa risosrsa a team risosrse
  --  prende il valore caricato nella combo da   RisorsaCaricaComboCoppiaLink
  -- e associa la risorsa alla TeamRisorsa creando una Coppia lincata con la stessa TeamRisosrsa

  PROCEDURE CreaCoppiaLinkataConRisorsa(iOidRisorsaTeam IN number, --oid della risorsateam
                                        iOidRisorsa     IN number,
                                        iUtente         IN varchar2,
                                        oMessaggio      out VARCHAR2) IS

    v_datainizio date;
    v_datafine   date;

  begin
    oMessaggio   := '';
    v_datainizio := null;
    v_datafine   := null;

    select s.datainizio, s.datafine
      into v_datainizio, v_datafine
      from assrisorseteam s
     where s.risorseteam = iOidRisorsaTeam;

    insert into assrisorseteam
      (oid, risorsa, risorseteam, datainizio, datafine)
    values
      ("sq_ASSRISORSETEAM".nextval,
       iOidRisorsa,
       iOidRisorsaTeam,
       v_datainizio,
       v_datafine);
    commit;

  EXCEPTION
    WHEN OTHERS THEN
      oMessaggio := 'Fallito: ' || SQLERRM;
      RAISE;

  END CreaCoppiaLinkataConRisorsa;

  --carico combo dei team associabili da Risorse
  -- nella detailView Risorsa, carico il comando combo Azione
  --  che prende la TeamRisorsa Selezionata e ci Associa la stessa risosrsa.
  PROCEDURE RisorsaCaricaComboCoppiaLink(iOidRisorsa IN number,
                                         iUtente     IN varchar2,
                                         oMessaggio  out VARCHAR2,
                                         IO_CURSOR   IN OUT T_CURSOR) IS

    V_CURSOR   T_CURSOR;
    v_mansione number;
    v_conto    number;
    v_CO       number := 0;
  BEGIN
    v_conto    := 0;
    v_mansione := 0;
    oMessaggio := '';

    select r.mansione, r.centrooperativo
      into v_mansione, v_CO
      from risorse r
     where r.oid = iOidRisorsa;

    OPEN V_CURSOR FOR
      select distinct x.nome || ' ' || x.cognome || ' (' || to_char(x.anno) || ')' as NomeCognomeRisorsaCapo,
                      x.OidRisorsaTeam
        from (

              select rt.oid OidRisorsaTeam, rt.anno, rs.nome, rs.cognome
                from risorseteam rt, assrisorseteam ass, risorse rs
               where rt.oid = ass.risorseteam
                 and rt.risorsacapo = rs.oid
                 and rs.mansione = v_mansione
                 and rs.centrooperativo = v_CO
                 and rt.oid not in
                     (select rt2.oid
                        from risorseteam rt2, assrisorseteam ass2
                       where rt2.oid = ass2.risorseteam
                         and ass2.risorsa = iOidRisorsa)
                 and rt.anno not in
                     (select /*rt.oid,*/
                       rt1.anno
                        from risorseteam rt1, assrisorseteam ass1
                       where rt1.oid = ass1.risorseteam
                         and ass1.risorsa = iOidRisorsa)
              --and rs.oid = 1
               group by rt.oid, rt.anno, rs.nome, rs.cognome
              having count(ass.oid) = 1) x;

    /*   select
    distinct
    r.nome||' '||r.cognome||' '||to_char(x.anno) as NomeCognomeRisorsaCapo,
    x.OidRisorsaTeam
    from risorse r,
    (select v.oid as OidRisorsaTeam, v.risorsacapo, v.anno from risorseteam v
    where v.oid in (
    select v.risorseteam from V_team_risorse v
    where v.mansione=v_mansione
    and v.nrrisorseteam=1
    and v.risorsacapo<>iOidRisorsa)) x
    where r.oid=x.risorsacapo ;*/

    IO_CURSOR := V_CURSOR;

  EXCEPTION
    WHEN OTHERS THEN
      oMessaggio := 'Fallito: ' || SQLERRM;
      RAISE;

  END RisorsaCaricaComboCoppiaLink;

  --public Dictionary<int, string>
  --RisorsaTeamCaricaCombo(int OidRisorsaTeam, string UserNameCorrente)
  -- dalla dettail di Team , deve caricare una combo con tutte le risorse possibili
  -- le risorse 1) non devono essere associate per l'anno della risosratea,
  -- non devono essere capo di altre risorse.
  PROCEDURE RisorsaTeamCaricaComboCLink(iOidRisorsaTeam IN number,
                                        iUtente         IN varchar2,
                                        oMessaggio      out VARCHAR2,
                                        IO_CURSOR       IN OUT T_CURSOR) IS

    V_CURSOR          T_CURSOR;
    v_mansione        number := 0;
    v_CO              number := 0;
    v_Anno            varchar2(10) := '0';
    v_RisorsaOid      number := 0;
    v_datamaxvalidita date;
    conto             number;
    v_conta           number := 0;

  BEGIN
    oMessaggio := '';
    v_mansione := 0;
    conto      := 0;
    ----  conta se è  gia risorsa tem coppia lincata
    select count(ar.risorsa)
      into v_conta
      from assrisorseteam ar, risorseTeam rt
     where ar.risorseteam = rt.oid
       and rt.oid = iOidRisorsaTeam --  9
    ;
    if v_conta = 0 then
      oMessaggio := '';
      OPEN V_CURSOR FOR
        select distinct r.nome || ' ' || r.cognome as NomeCognome,
                        r.oid as OidRisorsa
          from risorse r
         where r.oid in (select rt.risorsacapo
                           from risorseteam rt
                          where rt.oid = iOidRisorsaTeam);
      IO_CURSOR := V_CURSOR;
      return;
    end if;

    ---mansione della risorsacapo del team--
    select r.oid,
           r.mansione,
           r.centrooperativo,
           (select rt.anno
              from risorseteam rt
             where rt.oid = iOidRisorsaTeam) anno
      into v_RisorsaOid, v_mansione, v_CO, v_Anno
      from risorse r
     where r.oid = (select distinct rt.risorsacapo
                      from risorseteam rt
                     where rt.oid = iOidRisorsaTeam);
    /*   select distinct v.mansione, v.datamaxvalidita
    into v_mansione,v_datamaxvalidita
    from V_team_risorse v
    where v.risorseteam=iOidRisorsaTeam
    and v.risorsacapo<>0
    ;*/
    -- tagliere tutte le risorse associate o capo nell0anno [Anno]
    OPEN V_CURSOR FOR
      select distinct x.nome || ' ' || x.cognome || ' ' || x.oid || ' ' ||
                      x.mansione || ' ' || x.centrooperativo as NomeCognome,
                      x.oid as OidRisorsa
        from risorse x
       where x.oid in (select r.oid
                         from RISORSE r
                        where r.mansione = v_mansione --69
                          and r.centrooperativo = v_CO --1
                       minus (select rta.risorsa
                               from RISORSETEAM rt, ASSRISORSETEAM rta
                              where rt.oid = rta.risorseteam
                                and rt.anno = v_Anno

                             union
                             select rt.risorsacapo
                               from RISORSETEAM rt, ASSRISORSETEAM rta
                              where rt.oid = rta.risorseteam
                                and rt.anno = v_Anno));

    IO_CURSOR := V_CURSOR;
  EXCEPTION
    WHEN OTHERS THEN
      oMessaggio := 'Fallito: ' || SQLERRM;
      RAISE;

  END RisorsaTeamCaricaComboCLink;

  PROCEDURE AssociazioneCaricoCapacita(iOidRisorsaTeam IN number,
                                       iOIdGhost       IN number,
                                       iUtente         IN varchar2,
                                       oMessaggio      out VARCHAR2) IS

    v_min          number;
    v_max          number;
    v_media        number;
    v_moda         number;
    v_mediana      number;
    TotaleGhost    number;
    GhostAssegnati number;
    RegPiano       number;
  begin
    oMessaggio := '';
    v_min      := 0;
    v_max      := 0;
    v_media    := 0;
    v_moda     := 0;
    v_mediana  := 0;

    select case
             when g.min is null then
              0
             else
              g.min
           end as min_,
           case
             when g.max is null then
              0
             else
              g.max
           end as max_,
           case
             when g.media is null then
              0
             else
              g.media
           end as media_,
           case
             when g.moda is null then
              0
             else
              g.moda
           end as moda_,
           case
             when g.mediana is null then
              0
             else
              g.mediana
           end as mediana_
      into v_min, v_max, v_media, v_moda, v_mediana
      from mpghost g
     where g.oid = iOIdGhost;

    -- azzero enentuale precedente associazione
    update risorseteam r
       set r.min     = 0,
           r.max     = 0,
           r.media   = 0,
           r.moda    = 0,
           r.mediana = 0,
           r.mpghost = null
     where r.mpghost = iOIdGhost;
    commit;

    update risorseteam r
       set r.min     = v_min,
           r.max     = v_max,
           r.media   = v_media,
           r.moda    = v_moda,
           r.mediana = v_mediana,
           r.mpghost = iOIdGhost
     where r.oid = iOidRisorsaTeam;
    commit;
    --- verifica se l'associazione è parziale/totale, imposta lo stato nel registro di pianificazione
    select g.mpregpianificazione
      into RegPiano
      from mpghost g
     where g.oid = iOIdGhost;

    select count(t.oid)
      into TotaleGhost
      from mpghost t
     where t.mpregpianificazione = RegPiano;
    --    (select g.mpregpianificazione from mpghost g where g.oid = iOIdGhost);

    select count(rt.oid)
      into GhostAssegnati
      from risorseteam rt,
           (select t.oid
              from mpghost t
             where t.mpregpianificazione = RegPiano) gh --  (select g.mpregpianificazione from mpghost g where g.oid = iOIdGhost)) gh
     where rt.mpghost = gh.oid;

    /* if (TotaleGhost <= GhostAssegnati) then
      -- assegnazione parziale
      \* select   sp.nextazione
       into NextStato
       from Mpstatopianifica sp, Mpregpianificazione r
      where r.mpstatopianifica = sp.oid
        and r.oid = iRegPiano;*\

      update mpregpianificazione rp
         set rp.mpstatopianifica = 15 -- assegnazione totale dei ghost
       where rp.oid = RegPiano;
    else
      -- assegnazione totale
      update mpregpianificazione rp
         set rp.mpstatopianifica = 14 -- assegnazione parziale dei ghost
       where rp.oid = RegPiano;
    end if;*/

  EXCEPTION
    WHEN OTHERS THEN
      oMessaggio := 'Fallito: ' || SQLERRM;
      RAISE;

  END AssociazioneCaricoCapacita;

  /*-------------------------------------------------------
  PROCEDURE GetRisorsexTask(iOidRdL IN VARCHAR2,
                            --iUtente   IN varchar2,
                            IO_CURSOR IN OUT T_CURSOR
                            --IO_CURSOR   IN OUT T_CURSOR
                            ) IS

    V_CURSOR T_CURSOR;
    \*l_text     VARCHAR2(32767) := iOidRdL || ',';
    l_idx      NUMBER;
    l_element  VARCHAR2(32767);
    v_edificio number;
    v_scenario number;
    sett       varchar2(4000);
    settcorr   varchar2(4000);*\

  BEGIN

    insert into tbl_sql
      (s_sql, data)
    values
      ('
  procedure GetRisorsexTask param iOidRdL ' || iOidRdL,
       sysdate);
    commit;

    open V_CURSOR for
      select v.oidrisorseteam
        from V_cr_co_ed_risorseteam v
       where v.oidedificio in
             (select rdl.edificio from rdl where oid = iOidRdL);

    IO_CURSOR := V_CURSOR;

    \*sett     := '';
    settcorr := '';
    LOOP
      l_idx := INSTR(l_text, ',');
      EXIT WHEN NVL(l_idx, 0) = 0;
      l_element := TRIM(SUBSTR(l_text, 1, l_idx - 1));
      l_text    := SUBSTR(l_text, l_idx + 1);

      INSERT INTO LIST_NR_TMP (NR) VALUES (to_number(l_element));
    END LOOP;

    select distinct rdl.edificio
      into v_edificio
      from rdl
     where rdl.oid in (select t.nr from LIST_NR_TMP t);

    select distinct v.oid_scenario
      into v_scenario
      from v_scenari_cluster_edifici v
     where v.oid_edificio = v_edificio;

    for rs in (select s.oid -- , s.risorsacapo
                 from risorseteam s
               \* select s.oid  from risorseteam s, mpghost m
               where s.mpghost=m.oid
               and m.scenario=v_scenario*\
               ) loop
      settcorr  := rs.oid || ',';
      sett      := sett || settcorr;
      oCriteria := Substr(sett, 1, Length(sett) - 1);

    end loop;*\

  END GetRisorsexTask;

  */

  -------------------------------------------------------
  PROCEDURE GetRisorsexTask(iOidRdL IN VARCHAR2,
                            --iUtente   IN varchar2,
                            IO_CURSOR IN OUT T_CURSOR
                            --IO_CURSOR   IN OUT T_CURSOR
                            ) IS

    V_CURSOR T_CURSOR;
    /*l_text     VARCHAR2(32767) := iOidRdL || ',';
    l_idx      NUMBER;
    l_element  VARCHAR2(32767);
    v_edificio number;
    v_scenario number;
    sett       varchar2(4000);
    settcorr   varchar2(4000);
    -----------------
    --create or replace view v_cr_co_ed_risorseteam as
select e.oid || 0 || rs.oid as codice,
       ap.descrizione as areadipolo,
       e.descrizione as edificio,
       e.oid as oidedificio,
       c.descrizione as commessa,
       cr.descrizione as centroopoerativo,
       rs.oid as oidrisorseteam
  from commesse        c,
       edificio        e,
       areadipolo      ap,
       centrooperativo cr,
       risorse         ri,
       risorseteam     rs
 where e.commessa = c.oid
   and ap.oid = c.areadipolo
   and cr.areadipolo = ap.oid
   and ri.centrooperativo = cr.oid
   and ri.oid = rs.risorsacapo;
    ---------------
    */

  BEGIN

    insert into tbl_sql
      (s_sql, data)
    values
      ('
  procedure GetRisorsexTask param iOidRdL ' || iOidRdL,
       sysdate);
    commit;

 /*   open V_CURSOR for
      select v.oidrisorseteam
        from V_cr_co_ed_risorseteam v
       where v.oidedificio in
             (select rdl.edificio from rdl where oid = iOidRdL);

    IO_CURSOR := V_CURSOR;*/
---------------------
 open V_CURSOR for
select distinct to_number( RISORSETEAM || '00' ||EDIFICIO ) codice,
                RISORSETEAM oidrisorseteam,
                RISORSACAPO--,
/*                EDIFICIO,
                INDIRIZZO,
                acos(round(sin(round(longitudine_edificio, 5)) *
                           sin(round(longitudine_risorsa, 5)) +
                           Cos(round(longitudine_edificio, 5)) *
                           cos(round(longitudine_risorsa, 5)) *
                           cos(round(latitudine_risorsa, 5) -
                               round(latitudine_edificio, 5)),
                           5)) * 6371 DIST*/
  from (select nvl(i.geolat, 41.904321) latitudine_edificio,
               nvl(i.geolng, 12.491455) longitudine_edificio,
               nvl(r.geolat, 41.904321) latitudine_risorsa,
               nvl(r.geolng, 12.491455) longitudine_risorsa,
               t.Oid RISORSETEAM,
               e.oid EDIFICIO,
               im.oid IMPIANTO,
               i.oid INDIRIZZO,
               risorsacapo RISORSACAPO
          from impianto       im,
               edificio       e,
               indirizzo      i,
               risorse        r,
               risorseteam    t,
               assrisorseteam art,
               COMMESSE        C,
               AREADIPOLO      A,
               CENTROOPERATIVO CO
        where im.edificio = e.oid
           and e.indirizzo = i.oid
           and r.oid = t.risorsacapo
           and art.risorseteam = t.oid
           and art.risorsa = r.oid
           and sysdate BETWEEN art.datainizio AND art.datafine
           and e.oid in
             (select rdl.edificio from rdl where oid = iOidRdL)
              ----
           AND E.COMMESSA = C.OID
           AND C.AREADIPOLO = A.OID
           AND CO.AREADIPOLO = A.OID
           AND R.CENTROOPERATIVO = CO.OID

        ----
        ) vv

;
    IO_CURSOR := V_CURSOR;
  -------------------
    /*sett     := '';
    settcorr := '';
    LOOP
      l_idx := INSTR(l_text, ',');
      EXIT WHEN NVL(l_idx, 0) = 0;
      l_element := TRIM(SUBSTR(l_text, 1, l_idx - 1));
      l_text    := SUBSTR(l_text, l_idx + 1);

      INSERT INTO LIST_NR_TMP (NR) VALUES (to_number(l_element));
    END LOOP;

    select distinct rdl.edificio
      into v_edificio
      from rdl
     where rdl.oid in (select t.nr from LIST_NR_TMP t);

    select distinct v.oid_scenario
      into v_scenario
      from v_scenari_cluster_edifici v
     where v.oid_edificio = v_edificio;

    for rs in (select s.oid -- , s.risorsacapo
                 from risorseteam s
               \* select s.oid  from risorseteam s, mpghost m
               where s.mpghost=m.oid
               and m.scenario=v_scenario*\
               ) loop
      settcorr  := rs.oid || ',';
      sett      := sett || settcorr;
      oCriteria := Substr(sett, 1, Length(sett) - 1);

    end loop;*/

  END GetRisorsexTask;




  PROCEDURE GetDistanzeRisorseTeam(iOidRisorsaTeam   IN number,
                                   iOidImpianto      IN number,
                                   oDistanza         out VARCHAR2,
                                   oIncaricoImpianto out VARCHAR2,
                                   oUltimoImpianto   out VARCHAR2,
                                   oRdLSospese       out varchar2,
                                   oRdLAssegnate     out varchar2,
                                   oMessaggio        out VARCHAR2
                                   --IO_CURSOR   IN OUT T_CURSOR
                                   ) IS

    V_CURSOR               T_CURSOR;
    v_RisorsaTeam          number;
    v_geolati_str          varchar2(100);
    v_geolngi_str          varchar2(100);
    v_geolati              number;
    v_geolngi              number;
    v_geolatr              number;
    v_geolngr              number;
    v_distanza             number;
    parametro              number;
    conta_nr_attivita_sosp number;
    conta_nr_attivita_ass  number;
    v_max                  number;
    ContaIndirizzo         number;
    ContaImpianto          number;
    ContaCluster           number;
    ContaClusterImpianto   number;
  BEGIN
    oMessaggio := '';
    select count(r.oid)
      into v_RisorsaTeam
      from risorseteam r
     where r.oid = iOidRisorsaTeam;

    if v_RisorsaTeam <> 0 then
      select count(rdl.oid)
        into conta_nr_attivita_sosp
        from rdl
       where rdl.risorsateam = iOidRisorsaTeam
         and rdl.statooperativo in
             (select s.oid
                from statooperativo s
               where s.statooperativo = 'Sospeso');

      oRdLSospese := to_char(conta_nr_attivita_sosp);

      select count(rdl.oid)
        into conta_nr_attivita_ass
        from rdl
       where rdl.risorsateam = iOidRisorsaTeam
         and rdl.statosmistamento in
             (select m.oid
                from statosmistamento m
               where m.statosmistamento = 'Assegnata');

      oRdLAssegnate := to_char(conta_nr_attivita_ass);

      select nvl(max(rdl.oid), 0)
        into v_max
        from rdl
       where rdl.risorsateam = iOidRisorsaTeam;

      if v_max <> 0 then
        select i.descrizione
          into oUltimoImpianto
          from impianto i
         where i.oid in
               (select rdl.impianto from rdl where rdl.oid = v_max);
      else
        oUltimoImpianto := 'Non Rilevato.';

      end if;

    else

      oRdLSospese     := '0';
      oRdLAssegnate   := '0';
      oUltimoImpianto := 'Non Rilevato.';
    end if;

    /*
    select distinct v.oid_scenario
    into v_scenario
    from v_scenari_cluster_impianti v
    where v.oid_impianto=iOidImpianto;*/
    --  public IEnumerable<RdL> RdlUltimoStatoOperativo { get; set; }
    --RdlUltimoStatoOperativo.Where(rdl => rdl.RisorseTeam.Oid ==
    --     this.Oid & rdl.UltimoStatoOperativo.Stato == "Sospeso").Count();
    --     // get { return RdlUltimoStatoSmistamento.Where(rdl => rdl.RisorseTeam.Oid ==
    --     this.Oid & rdl.UltimoStatoSmistamento.SSmistamento == "Assegnata").Count(); }
    ---   verifico se è stato associato un indirizzo all'impianto/edificio
    select count(i.oid)
      into ContaIndirizzo
      from impianto t, edificio e, indirizzo i
     where t.edificio = e.oid
       and e.indirizzo = i.oid
       and t.oid = iOidImpianto;
    /*   var milano = new { lat = 45.464194D, lon = 9.188132D };
    var roma = new { lat = 41.904321D, lon = 12.491455D };*/
    if ContaIndirizzo > 0 and v_RisorsaTeam <> 0 then
      select nvl(i.geolat, 41.904321), nvl(i.geolng, 12.491455)
        into v_geolati, v_geolngi
        from impianto t, edificio e, indirizzo i
       where t.edificio = e.oid
         and e.indirizzo = i.oid
         and t.oid = iOidImpianto;
      -- test -->  v_geolatr:= 41.904321  ; v_geolngr :=9.188132   ;
      -- se esiste il Team allora esiste necessariamente la risorsa capo!
      select nvl(r.geolat, 41.904321), nvl(r.geolng, 12.491455)
        into v_geolatr, v_geolngr
        from risorse r
       where r.oid in (select s.risorsacapo
                         from risorseteam s
                        where s.oid = iOidRisorsaTeam);
      -- test -->   v_geolatr:= 45.464194  ; v_geolngr :=9.188132   ;
      parametro := (1 / 180) * 3.14159265358979;
      --REPLACE('222tech', '2', '3'); would return '333tech') --v_geolati:=replace(v_geolati,',','.');
      v_geolati := v_geolati * parametro; --p1X
      v_geolngi := v_geolngi * parametro; --p1Y
      v_geolatr := v_geolatr * parametro; --p2X
      v_geolngr := v_geolngr * parametro; --p2Y
      select acos(sin(v_geolngi) * sin(v_geolngr) +
                  Cos(v_geolngi) * cos(v_geolngr) *
                  cos(v_geolatr - v_geolati)) * 6371 /** 1000*/
        into v_distanza
        from dual;

      oDistanza := 'km ' || to_char(round(v_distanza, 2));
    else
      oDistanza := 'non Calcolabile.';
    end if;

    select count(gi.edificio)
      into ContaImpianto
      from risorseteam       s,
           mpghost           g,
           mpghostdettaglio  gd,
           mpghostitinerante gi
     where s.mpghost = g.oid
       and gd.mpghost = g.oid
       and gi.mpghostdettaglio = gd.oid
       and s.oid = iOidRisorsaTeam;

    if ContaImpianto > 0 then

      select count(distinct c.oid), count(distinct t.oid)
        into ContaCluster, ContaClusterImpianto
        from edificio t, clusteredifici c
       where t.clusteredifici = c.oid
         and t.oid in ((select iOidImpianto from dual) union
                       (select gi.edificio
                          from risorseteam       s,
                               mpghost           g,
                               mpghostdettaglio  gd,
                               mpghostitinerante gi
                         where s.mpghost = g.oid
                           and gd.mpghost = g.oid
                           and gi.mpghostdettaglio = gd.oid
                           and s.oid = iOidRisorsaTeam));
      if ContaClusterImpianto = 1 then
        oIncaricoImpianto := 'Stesso Impianto';
      elsif ContaCluster = 1 then
        oIncaricoImpianto := 'Stesso Cluster';
      else
        oIncaricoImpianto := 'Stesso Scenario';
      end if;

    else
      oIncaricoImpianto := 'non Calcolabile.';
    end if;

  EXCEPTION
    WHEN OTHERS THEN
      oMessaggio := 'Fallito: ' || SQLERRM;
      RAISE;

  end GetDistanzeRisorseTeam;

  PROCEDURE NrInterventiInEdificio(iOidRisorsaTeam IN number,
                                   iOidEdificio    IN number,
                                   oNrInterventi   out VARCHAR2,
                                   oMessaggio      out VARCHAR2) IS

    V_CURSOR               T_CURSOR;
    v_RisorsaTeam          number;
    v_geolati_str          varchar2(100);
    v_geolngi_str          varchar2(100);
    v_geolati              number;
    v_geolngi              number;
    v_geolatr              number;
    v_geolngr              number;
    v_distanza             number;
    parametro              number;
    conta_nr_attivita_sosp number;
    conta_nr_attivita_ass  number;
    v_max                  number;
    ContaIndirizzo         number;
    ContaImpianto          number;
    ContaCluster           number;
    ContaClusterImpianto   number;
  BEGIN
    oMessaggio := '';
    select count(r.oid)
      into v_RisorsaTeam
      from risorseteam r
     where r.oid = iOidRisorsaTeam;

    if v_RisorsaTeam <> 0 then
      select count(rdl.oid)
        into conta_nr_attivita_sosp
        from rdl
       where rdl.risorsateam = iOidRisorsaTeam
         and rdl.statooperativo in
             (select s.oid
                from statooperativo s
               where s.statooperativo = 'Sospeso');
    end if;
    /*
    \*
    select distinct v.oid_scenario
    into v_scenario
    from v_scenari_cluster_impianti v
    where v.oid_impianto=iOidImpianto;*\
    --  public IEnumerable<RdL> RdlUltimoStatoOperativo { get; set; }
    --RdlUltimoStatoOperativo.Where(rdl => rdl.RisorseTeam.Oid ==
    --     this.Oid & rdl.UltimoStatoOperativo.Stato == "Sospeso").Count();
    --     // get { return RdlUltimoStatoSmistamento.Where(rdl => rdl.RisorseTeam.Oid ==
    --     this.Oid & rdl.UltimoStatoSmistamento.SSmistamento == "Assegnata").Count(); }
    ---   verifico se è stato associato un indirizzo all'impianto/edificio
    select count(i.oid)
      into ContaIndirizzo
      from impianto t, edificio e, indirizzo i
     where t.edificio = e.oid
       and e.indirizzo = i.oid
       and t.oid = iOidImpianto;
    \*   var milano = new { lat = 45.464194D, lon = 9.188132D };
    var roma = new { lat = 41.904321D, lon = 12.491455D };*\
    if ContaIndirizzo > 0 and v_RisorsaTeam <> 0 then
      select nvl(i.geolat, 41.904321), nvl(i.geolng, 12.491455)
        into v_geolati, v_geolngi
        from impianto t, edificio e, indirizzo i
       where t.edificio = e.oid
         and e.indirizzo = i.oid
         and t.oid = iOidImpianto;
      -- test -->  v_geolatr:= 41.904321  ; v_geolngr :=9.188132   ;
      -- se esiste il Team allora esiste necessariamente la risorsa capo!
      select nvl(r.geolat, 41.904321), nvl(r.geolng, 12.491455)
        into v_geolatr, v_geolngr
        from risorse r
       where r.oid in (select s.risorsacapo
                         from risorseteam s
                        where s.oid = iOidRisorsaTeam);
      -- test -->   v_geolatr:= 45.464194  ; v_geolngr :=9.188132   ;
      parametro := (1 / 180) * 3.14159265358979;
      --REPLACE('222tech', '2', '3'); would return '333tech') --v_geolati:=replace(v_geolati,',','.');
      v_geolati := v_geolati * parametro; --p1X
      v_geolngi := v_geolngi * parametro; --p1Y
      v_geolatr := v_geolatr * parametro; --p2X
      v_geolngr := v_geolngr * parametro; --p2Y
      select acos(sin(v_geolngi) * sin(v_geolngr) +
                  Cos(v_geolngi) * cos(v_geolngr) *
                  cos(v_geolatr - v_geolati)) * 6371 \** 1000*\
        into v_distanza
        from dual;

      oDistanza := 'km ' || to_char(round(v_distanza, 2));
    else
      oDistanza := 'non Calcolabile.';
    end if;

    select count(gi.edificio)
      into ContaImpianto
      from risorseteam       s,
           mpghost           g,
           mpghostdettaglio  gd,
           mpghostitinerante gi
     where s.mpghost = g.oid
       and gd.mpghost = g.oid
       and gi.mpghostdettaglio = gd.oid
       and s.oid = iOidRisorsaTeam;

    if ContaImpianto > 0 then

      select count(distinct c.oid), count(distinct t.oid)
        into ContaCluster, ContaClusterImpianto
        from edificio t, clusteredifici c
       where t.clusteredifici = c.oid
         and t.oid in ((select iOidImpianto from dual) union
                       (select gi.edificio
                          from risorseteam       s,
                               mpghost           g,
                               mpghostdettaglio  gd,
                               mpghostitinerante gi
                         where s.mpghost = g.oid
                           and gd.mpghost = g.oid
                           and gi.mpghostdettaglio = gd.oid
                           and s.oid = iOidRisorsaTeam));
      if ContaClusterImpianto = 1 then
        oIncaricoImpianto := 'Stesso Impianto';
      elsif ContaCluster = 1 then
        oIncaricoImpianto := 'Stesso Cluster';
      else
        oIncaricoImpianto := 'Stesso Scenario';
      end if;

    else
      oIncaricoImpianto := 'non Calcolabile.';
    end if;*/

  EXCEPTION
    WHEN OTHERS THEN
      oMessaggio := 'Fallito: ' || SQLERRM;
      RAISE;

  end NrInterventiInEdificio;

  PROCEDURE AggiornaRdLbySSmistamento(iOidRegRdl in number,
                                      iUtente in varchar2,
                                      iTipo      IN varchar2,
                                      oMessaggio out varchar2) is
    error EXCEPTION;
    --NewRegRdL        number;
    --RegRdLisfatto    number;
    /*
    V_descrdl        varchar2(4000);
    V_descregdl      varchar2(100);
    v_lungdescrdl    number := 0;
    v_lungdescregrdl number := 0;
    v_sommadescr     number := 0;
    v_testata        varchar2(10) := 'REG. TT';
   */
    v_categoria      number := 0;
    v_odl            number := 0;
    v_nr_odl         number := 0;
    v_oMessaggio       varchar2(100) := '';
    v_datassegnazioneodl date :=null;
    v_datapianificata  date :=null;
    v_dataupdate       date:=null;
    v_data_sopralluogo date:=null;
    v_data_azioni_tampone date:=null;
    v_data_inizio_lavori date:=null;
    v_datariavvio      date :=null;
    v_datafermo        date :=null;
    v_datacompletamento date :=null;
    v_notecompletamento varchar2(1000);
    --v_utente           varchar2(100);
    v_SSmistamento     number := 0;
    v_SOperativo       number := 0;
    v_regrdl           number;
    v_iOidREGSMDETmax  number := 0;
    v_iOidREGSODETmax  number := 0;
    appo_odl           number := 0;
    v_stsm_pre         number;
    v_stop_pre         number;
    v_risorsateam      number := 0;
    v_DescrCambioStato varchar2(1000);
    v_pcrappproblema   number;
    v_pcrprobcausa     number;
    v_nrrdl            number:=0;
    v_risorsacapo      number;
    v_nrrdlch          number:=0;
    v_utente           varchar2(100):=iUtente;
    --_stop_att number;
    --vv_stsm_att number;

  begin
   insert into tbl_sql
     (s_sql, data)
   values
     ('AggiornaRdLbySSmistamento iOidRegRdl : '||iOidRegRdl||
     ' iTipo '||iTipo

     , sysdate);
   commit;


   begin
      --        iTipo    IN varchar2: RdL, RegistroRdL

        update regrdl
        set regrdl.ultimoutente=iUtente,
            regrdl.ultimadataaggiornamento=regrdl.dataupdate
        where regrdl.oid = iOidRegRdl;
        commit;


      if upper(iTipo) = upper('RegRdL') then


        select regrdl.ultimostatosmistamento,
               regrdl.ultimostatooperativo,
               regrdl.dataupdate,
               regrdl.utente,
               regrdl.risorsateam,
               regrdl.categoria,
               regrdl.data_sopralluogo,
               regrdl.data_azioni_tampone,
               regrdl.data_inizio_lavori,
               regrdl.datariavvio,
               regrdl.datafermo,
               regrdl.datacompletamento,
               regrdl.notecompletamento,
               regrdl.dataassegnazioneodl
          into v_SSmistamento,
               v_SOperativo,
               v_dataupdate,
               v_utente,
               v_risorsateam,
               v_categoria,
               v_data_sopralluogo,
               v_data_azioni_tampone,
               v_data_inizio_lavori,
               v_datariavvio,
               v_datafermo,
               v_datacompletamento,
               v_notecompletamento,
               v_datapianificata

          from regrdl
         where regrdl.oid = iOidRegRdl;

        v_regrdl := iOidRegRdl;


      ------------stato smistamento-------------------
      select case
                 when max(t.oid) is null then
                  0
                 else
                  max(t.oid)
               end
          into v_iOidREGSMDETmax
          from regsmistamentodett t
         where t.regrdl = v_regrdl;

      select t.statosmistamento
          into v_stsm_pre
          from regsmistamentodett t
         where t.oid = v_iOidREGSMDETmax;

      if v_stsm_pre <> v_SSmistamento then

          UPDDELTATREGSMPREC(v_iOidREGSMDETmax, v_dataupdate, v_oMessaggio);
          insert into regsmistamentodett
            (oid, regrdl, statosmistamento, dataora,
            descrizione,
            risorsateam,
            utente)
          values
            ("sq_REGSMISTAMENTODETT".Nextval,
             v_regrdl,
             v_SSmistamento,
             v_dataupdate,
             --'RegRdL nr '||v_regrdl||
             ' Variazione Stato Smistamento da ' ||
             (select sm.statosmistamento
                from statosmistamento sm
               where sm.oid = v_stsm_pre) || ' a ' ||
             (select sm.statosmistamento
                from statosmistamento sm
               where sm.oid = v_SSmistamento),
              v_risorsateam ,
              v_utente

               );
          commit;

       --update delle rdl solo con stato smistamento precedente
      for nr in (select rdl.oid from rdl
          where rdl.regrdl = iOidRegRdl
                and rdl.statosmistamento=v_stsm_pre) loop
          update rdl
             set rdl.statosmistamento  = v_SSmistamento,
                 rdl.statooperativo    = v_SOperativo,
                 rdl.dataupdate        = v_dataupdate,
                 rdl.utenteinserimento = v_utente,
                 rdl.risorsateam       = v_risorsateam,
                 rdl.datafermo         = v_datafermo,
                 rdl.datariavvio       = v_datariavvio,
                 rdl.data_sopralluogo  =v_data_sopralluogo,
                 rdl.data_azioni_tampone= v_data_azioni_tampone,
                 rdl.data_inizio_lavori= v_data_inizio_lavori,
                 rdl.datacompletamento = v_datacompletamento,
                 rdl.notecompletamento = v_notecompletamento,
                 rdl.datapianificata  = v_datapianificata
           where rdl.oid = nr.oid;
          commit;
        end loop;
      end if;

      ------------stato operativo-------------------
     --select per sapere se non ci sono per regrdl
     --non ci sono registrioperativi
      select case
                 when max(r.oid) is null then
                  0
                 else
                  max(r.oid)
               end
          into v_iOidREGSODETmax
          from regoperativodettaglio r
         where r.regrdl = v_regrdl;

      if v_iOidREGSODETmax <> 0 then

      select r.statooperativo
            into v_stop_pre
            from regoperativodettaglio r
           where r.oid = v_iOidREGSODETmax;

      if v_stop_pre <> v_SOperativo then
            v_DescrCambioStato := getdesccambiosoperativo(v_stop_pre,
                                                          v_SOperativo);
            UPDDELTATREGSOPREC(v_iOidREGSODETmax,
                               v_dataupdate,
                               v_oMessaggio);
            insert into regoperativodettaglio
              (oid,
               regrdl,
               statooperativo,
               dataora,
               descrizione,
               flagcompletatoso,
               utente
               )
            values
              ("sq_REGOPERATIVODETTAGLIO".Nextval,
               v_regrdl,
               v_SOperativo,
               v_dataupdate,
               v_DescrCambioStato,
               (select case
                         when so.oid = 11 then
                          1
                         else
                          0
                       end
                  from statooperativo so
                 where so.oid = v_SOperativo),
              v_utente   );
            commit;
          end if;
      else
      --va inserito un registrooperativo
      insert into regoperativodettaglio
            (oid,
             regrdl,
             statooperativo,
             dataora,
             descrizione,
             flagcompletatoso,
             utente)
          values
            ("sq_REGOPERATIVODETTAGLIO".Nextval,
             v_regrdl,
             v_SOperativo,
             v_dataupdate,
             --'RegRdL nr '||v_regrdl||
             ' Stato Operativo ' ||
             (select so.codstato
                from statooperativo so
               where so.oid = v_SOperativo),
             (select case
                       when so.oid = 11 then
                        1
                       else
                        0
                     end
                from statooperativo so
               where so.oid = v_SOperativo)
               ,v_utente
               ); --non c'è stato operativo precedente inserimento e basta

      end if;
      --***aggiornamenti registi smist e op e chiamate al completamento

      if v_SSmistamento = 2 then

           insert into tbl_sql
     (s_sql, data)
   values
     ('AggiornaRdLbySSmistamento stato smist 2'
     , sysdate);
      commit;
         select
         count(oid) into v_nr_odl
         from odl
         where odl.regrdl=v_regrdl;
        if  v_nr_odl =0 then
       --inserimento odL
        select "sq_ODL".Nextval into appo_odl from dual;
        insert into odl
          (OID,
           DESCRIPTION,
           TIPOODL,
           STATOOPERATIVO,
           DATAEMISSIONE,
           REGRDL)
        values
          (appo_odl,
           (select regrdl.descrizione from regrdl where oid = v_regrdl),
           1,
           v_SOperativo,
           v_dataupdate,
           v_regrdl);
        commit;

        update rdl set rdl.odl = appo_odl where rdl.regrdl = v_regrdl;
        commit;
        end if;
        ---aggiornamento risorsateam sul registro smistamento   alla prima assegnazione team---
        select case
                 when max(t.oid) is null then
                  0
                 else
                  max(t.oid)
               end
          into v_iOidREGSMDETmax
          from regsmistamentodett t
         where t.regrdl = v_regrdl;

        update regsmistamentodett r
           set r.risorsateam = v_risorsateam
         where r.oid = v_iOidREGSMDETmax;
        commit;
      end if;


      end if;

      ----------*/-------------------------------------------------------------------------
      if upper(iTipo) = upper('RdL') then

        select rdl.statosmistamento,
               rdl.statooperativo,
               rdl.regrdl,
               rdl.dataupdate,
               rdl.risorsateam,
               rdl.pcrappproblema,
               rdl.pcrprobcausa,
               rdl.categoria,
               rdl.datafermo,
               rdl.datariavvio,
               rdl.data_sopralluogo,
               rdl.data_azioni_tampone,
               rdl.data_inizio_lavori,
               rdl.datacompletamento,
               rdl.notecompletamento,
               rdl.datapianificata
          into
               v_SSmistamento,
               v_SOperativo,
               v_regrdl,
               v_dataupdate,
               v_risorsateam,
               v_pcrappproblema,
               v_pcrprobcausa,
               v_categoria,
               v_datafermo,
               v_datariavvio,
               v_data_sopralluogo,
               v_data_azioni_tampone,
               v_data_inizio_lavori,
               v_datacompletamento,
               v_notecompletamento,
               v_datassegnazioneodl
        from rdl
        where rdl.oid = iOidRegRdl;
      --fix bug applicazione--
      select
      nvl(nvl(rdl.risorsateam, r.risorsateam),minrisorsateam)
      into v_risorsateam
      from regrdl r, rdl, edificio e, centrooperativo c,
      (
      select
      min(rt.oid) as minrisorsateam, c.oid
      from risorseteam rt, risorse ri, centrooperativo c
      where c.oid=ri.centrooperativo
      and rt.risorsacapo=ri.oid
      group by c.oid
      ) ris
      where r.oid=rdl.regrdl
      and e.oid=rdl.edificio
      and e.centrooperativo=c.oid
      and ris.oid=c.oid
      and rdl.oid = iOidRegRdl
      ;

        select rt.risorsacapo
        into v_risorsacapo
        from risorseteam rt
        where rt.oid = v_risorsateam;



        --il regrdl ha 1 una sola rdl e allora pùò essere aggiornato totalmente


        update regrdl
           set regrdl.ultimostatosmistamento = v_SSmistamento,
               regrdl.ultimostatooperativo   = v_SOperativo,
               regrdl.dataupdate             = v_dataupdate,
               regrdl.risorsateam            = v_risorsateam,
               regrdl.datafermo              = v_datafermo,
               regrdl.datariavvio            = v_datariavvio,
               regrdl.data_sopralluogo       = v_data_sopralluogo,
               regrdl.data_azioni_tampone    = v_data_azioni_tampone,
               regrdl.data_inizio_lavori     = v_data_inizio_lavori,
               regrdl.datacompletamento      = v_datacompletamento,
               regrdl.notecompletamento      = v_notecompletamento,
               regrdl.dataassegnazioneodl    = v_datassegnazioneodl

         where regrdl.oid in
               (select rdl.regrdl from rdl where rdl.oid = iOidRegRdl);
        commit;

         ------------stato smistamento-------------------
      select case
                 when max(t.oid) is null then
                  0
                 else
                  max(t.oid)
               end
          into v_iOidREGSMDETmax
          from regsmistamentodett t
         where t.regrdl = v_regrdl;

      select t.statosmistamento
          into v_stsm_pre
          from regsmistamentodett t
         where t.oid = v_iOidREGSMDETmax;

   if v_stsm_pre <> v_SSmistamento then

          UPDDELTATREGSMPREC(v_iOidREGSMDETmax, v_dataupdate, v_oMessaggio);
          insert into regsmistamentodett
          (oid, regrdl, statosmistamento, dataora,
          descrizione,regsmistamentodett.risorsateam,
          utente
          )
          values
          ("sq_REGSMISTAMENTODETT".Nextval,
          v_regrdl,
          v_SSmistamento,
          v_dataupdate,
          --'RegRdL nr '||v_regrdl||
          ' Variazione Stato Smistamento da ' ||
          (select sm.statosmistamento
          from statosmistamento sm
          where sm.oid = v_stsm_pre) || ' a ' ||
          (select sm.statosmistamento
          from statosmistamento sm
          where sm.oid = v_SSmistamento),
          v_risorsateam,
          v_utente
          );
          commit;


      end if;

      ------------stato operativo-------------------
     --select per sapere se non ci sono per regrdl
     --non ci sono registrioperativi
    if  v_SSmistamento<>1 then
            select case
                       when max(r.oid) is null then
                        0
                       else
                        max(r.oid)
                     end
                into v_iOidREGSODETmax
                from regoperativodettaglio r
               where r.regrdl = v_regrdl;

            if v_iOidREGSODETmax <> 0 then

            select r.statooperativo
                  into v_stop_pre
                  from regoperativodettaglio r
                 where r.oid = v_iOidREGSODETmax;

            if v_stop_pre <> v_SOperativo then
                  v_DescrCambioStato := getdesccambiosoperativo(v_stop_pre,
                                                                v_SOperativo);
                  UPDDELTATREGSOPREC(v_iOidREGSODETmax,
                                     v_dataupdate,
                                     v_oMessaggio);
                  insert into regoperativodettaglio
                    (oid,
                     regrdl,
                     statooperativo,
                     dataora,
                     descrizione,
                     flagcompletatoso,
                     utente)
                  values
                    ("sq_REGOPERATIVODETTAGLIO".Nextval,
                     v_regrdl,
                     v_SOperativo,
                     v_dataupdate,
                     v_DescrCambioStato,
                     (select case
                               when so.oid = 11 then
                                1
                               else
                                0
                             end
                        from statooperativo so
                       where so.oid = v_SOperativo)
                     ,v_utente
                       );
                  commit;
                end if;
            else
            --va inserito un registrooperativo
            insert into regoperativodettaglio
                  (oid,
                   regrdl,
                   statooperativo,
                   dataora,
                   descrizione,
                   flagcompletatoso,
                   utente)
                values
                  ("sq_REGOPERATIVODETTAGLIO".Nextval,
                   v_regrdl,
                   v_SOperativo,
                   v_dataupdate,
                   --'RegRdL nr '||v_regrdl||
                   ' Stato Operativo ' ||
                   (select so.codstato
                      from statooperativo so
                     where so.oid = v_SOperativo),
                   (select case
                             when so.oid = 11 then
                              1
                             else
                              0
                           end
                      from statooperativo so
                     where so.oid = v_SOperativo)
                     , v_utente); --non c'è stato operativo precedente inserimento e basta
              commit;
            end if;
            --***aggiornamenti registi smist e op e chiamate al completamento

           end if;

        end if;

  if v_SSmistamento = 2 then

           insert into tbl_sql
     (s_sql, data)
   values
     ('AggiornaRdLbySSmistamento stato smist 2'
     , sysdate);
      commit;

      --codice inserito per evitare di generare altre odl
         select
         count(oid) into v_nr_odl
         from odl
         where odl.regrdl=v_regrdl;
        if  v_nr_odl =0 then
       --inserimento odL
        select "sq_ODL".Nextval into appo_odl from dual;
        insert into odl
          (OID,
           DESCRIPTION,
           TIPOODL,
           STATOOPERATIVO,
           DATAEMISSIONE,
           REGRDL)
        values
          (appo_odl,
           (select regrdl.descrizione from regrdl where oid = v_regrdl),
           1,
           v_SOperativo,
           v_dataupdate,
           v_regrdl);
        commit;

        update rdl set rdl.odl = appo_odl where rdl.regrdl = v_regrdl;
        commit;
        end if;
        ---aggiornamento risorsateam sul registro smistamento   alla prima assegnazione team---
        select case
                 when max(t.oid) is null then
                  0
                 else
                  max(t.oid)
               end
          into v_iOidREGSMDETmax
          from regsmistamentodett t
         where t.regrdl = v_regrdl;

        update regsmistamentodett r
           set r.risorsateam = v_risorsateam
         where r.oid = v_iOidREGSMDETmax;
        commit;
      end if;


      if v_SSmistamento = 4 then

      insert into tbl_sql
     (s_sql, data)
   values
     ('AggiornaRdLbySSmistamento stato smist 4 per tipo '  ||iTipo
     || ''
     , sysdate);
      commit;
        update odl
        set odl.qtyopenrdl     =
        (select count(rdl.oid) as nr
        from rdl
        where rdl.regrdl = v_regrdl),
        odl.date_completed  = v_dataupdate,
        odl.totminimpegnati =
        (select sum(d.deltatempo) as nr
        from regsmistamentodett d
        where d.regrdl = v_regrdl),
        odl.totrisorse     =
        (select case
        when count(a.risorseteam) = 0 then
        1
        else
        count(a.risorseteam)
        end
        from assrisorseteam a
        where a.risorseteam in
        (select f.risorsateam
        from regrdl f
        where f.oid = v_regrdl)),
        odl.statooperativo  = 11,
        odl.dataemissione   = v_datassegnazioneodl
        where odl.regrdl = v_regrdl;
        commit;
        --viene inserito il kpi se manutenzione a guasto
      /*  if v_categoria=4 then
        --inskpimtbf(v_regrdl, v_oMessaggio);
        end if;*/
        --viene clonato il registro se manutenzione programmata
       /* if v_categoria=1 then
                clona_regrdl_mp(v_regrdl,v_oMessaggio);
         end if;*/
      end if;

      if v_SSmistamento = 6 then
        update odl set odl.statooperativo = 12 where odl.regrdl = v_regrdl;
        commit;
        update risorseteam r set r.regrdl = null where r.regrdl = v_regrdl;
        commit;
      end if;

      if v_SSmistamento = 7 then
        update odl set odl.statooperativo = 13 where odl.regrdl = v_regrdl;
        commit;
        update risorseteam r set r.regrdl = null where r.regrdl = v_regrdl;
        commit;
      end if;

      if v_SSmistamento = 11 then

        update odl
           set odl.statooperativo = v_SOperativo
         where odl.regrdl = v_regrdl;
        commit;
        update risorseteam r
        set r.regrdl = null
        where r.regrdl = v_regrdl;
        commit;
        update risorse r
           set r.disponibile   = 0,
               r.geoultimadataaggiornamento = v_dataupdate
         where r.oid = v_risorsacapo;
        commit;
      end if;


    UPDATETEAMCOUNT(v_risorsateam,v_oMessaggio );


    EXCEPTION
      WHEN OTHERS THEN
        oMessaggio := 'Errore imprevisto in PK_MPASSCARICOCAPACITA.CreaRegRdLbyRdL : ' ||
                      SQLERRM;
    END;

  end AggiornaRdLbySSmistamento;

  PROCEDURE CreaRegRdLbyRdL
    (iOidRdl in number,iUtente in varchar2, oMessaggio out varchar2)
     is
    -- error EXCEPTION;
    NewRegRdL        number;
    RegRdLisfatto    number;
    V_descrdl        varchar2(4000);
    V_descregdl      varchar2(100);
    v_lungdescrdl    number := 0;
    v_lungdescregrdl number := 0;
    v_sommadescr     number := 0;
    v_testata        varchar2(10) := 'REG. TT';
    v_categoria      number := 0;
    v_contorichiedenteman number := 0;
    v_richiedente   number:=0;
    v_commesse      number:=0;
    vnewoidref number:=0;
  begin
    begin

    insert into tbl_sql
     (s_sql, data)
   values
     ('CreaRegRdLbyRdL iOidRdl : '||iOidRdl
      , sysdate);
   commit;

     --RICHIEDENTE--
        select
        count(r.rowid) into v_contorichiedenteman
        from rdl t, edificio e, commesse c, richiedente r
        where
        t.edificio=e.oid
        and e.commessa=c.oid
        and r.commesse=c.oid
        and trim(r.nomecognome)=trim('Manutentore')
        and t.oid=iOidRdl
        ;

        if v_contorichiedenteman=0
        then
        --prendo oid commessa --
        select
        c.oid into v_commesse
        from rdl t, edificio e, commesse c
        where
        t.edificio=e.oid
        and e.commessa=c.oid
        and t.oid=iOidRdl
        ;
         --inserisco richiedente Manutentore --
        select "sq_RICHIEDENTE".Nextval into vnewoidref from dual;

        insert into richiedente
          (oid, nomecognome, tiporichiedente, commesse)
        values
          (vnewoidref,'Manutentore' , 264,v_commesse);
        commit;
        end if;

        select
        min(r.oid) into v_richiedente
        from rdl t, edificio e, commesse c, richiedente r
        where
        t.edificio=e.oid
        and e.commessa=c.oid
        and r.commesse=c.oid
        and trim(r.nomecognome)=trim('Manutentore')
        and t.oid=iOidRdl
        ;

      /*      --- crea la reg rdl by registro.
         select count(*)  into RegRdLisfatto
            from rdl r
           where oid = iOidRdl
           and   r.regrdl = null
           ;
      */
      -- if RegRdLisfatto = 0 then

      -- AGGIORNARE  FARE UPADATE : AGGIUNGERE INSERT REGOPERATIVO E SMISTAMENTO

      select rdl.categoria into v_categoria
      from rdl where oid = iOidRdl;
      if v_categoria = 5 then
        v_testata := 'REG. MP';
      update rdl t
      set t.richiedente= v_richiedente
      where t.oid= iOidRdl;
      commit;
      end if;

      if v_categoria = 3 then

      update rdl t
      set t.richiedente= v_richiedente
      where t.oid= iOidRdl;
      commit;
      end if;



      select "sq_REGRDL".Nextval into NewRegRdL from dual;

      insert into REGRDL
        (OID,
         Categoria,
         DESCRIZIONE,
         UTENTE,
         ULTIMADATAAGGIORNAMENTO,
         DATA_CREAZIONE_RDL,
         ULTIMOSTATOSMISTAMENTO,
         PRIORITA,
         risorsateam,
         apparato,
         regrdl.data_inizio_lavori,
         REGRDL.Datafermo,
         REGRDL.Datariavvio,
         REGRDL.Pcrappproblema,
         REGRDL.Pcrprobcausa)

        (
        select NewRegRdL,
                rdl.categoria,
                v_testata || '(' || NewRegRdL || ') '||
                 substr(rdl.descrizione,1,3900)
                /* || ' EDIF. ' ||
                (select e.cod_descrizione
                   from edificio e
                  where e.oid in
                        (select impianto.edificio
                           from impianto
                          where oid in (select r.impianto
                                          from rdl r
                                         where oid = iOidRdl))) ||
                ' TIPO MANUT. ' ||
                (select categoria.cod_descrizione
                   from categoria
                  where oid in
                        (select r.categoria from rdl r where oid = iOidRdl)) ||
                ' DESC ATT: '*/

                ,
                UTENTEINSERIMENTO,
                DATAUPDATE,
                DATACREAZIONE,
                1,
                rdl.priorita,
                rdl.risorsateam,
                rdl.apparato,
                rdl.data_inizio_lavori,
                rdl.datafermo,
                rdl.datariavvio,
                rdl.pcrappproblema,
                rdl.pcrprobcausa
           from rdl
          where oid = iOidRdl);

      update rdl
         set regrdl = NewRegRdL, statosmistamento = 1
       where oid = iOidRdl;
      commit;

      insert into regsmistamentodett
        (oid, descrizione, statosmistamento, dataora, regrdl,
        utente
         )
      values
        ("sq_REGSMISTAMENTODETT".nextval,
         --'RegRdL nr ' || TO_CHAR(NewRegRdL)||
         ' Stato Smistamento In Attesa di Assegnazione',
         1,
         (select r.data_creazione_rdl from regrdl r where r.oid = NewRegRdL),
         NewRegRdL,
         iutente
         );
      commit;

      select rdl.descrizione, length(rdl.descrizione)
        into V_descrdl, v_lungdescrdl
        from rdl
       where oid = iOidRdl;
      select regrdl.descrizione, length(regrdl.descrizione)
        into V_descregdl, v_lungdescregrdl
        from regrdl
       where oid = NewRegRdL;
      v_sommadescr := v_lungdescrdl + v_lungdescregrdl;
      if (v_sommadescr <= 100) then
        update regrdl
           set regrdl.descrizione = V_descregdl || V_descrdl
         where regrdl.oid = NewRegRdL;
        commit;
      else
        V_descrdl := substr(V_descregdl || V_descrdl, 0, 98) || '..';
        update regrdl
           set regrdl.descrizione = V_descrdl
         where regrdl.oid = NewRegRdL;
        commit;
      end if;

      for nn in (select /*e.oid as edificio, t.oid as commesse, */
                 distinct t.m_datafermo,
                          t.m_datariavvio,
                          t.m_data_sopralluogo,
                          t.m_data_azioni_tampone,
                          t.m_data_inizio_lavori,
                          t.m_datacompletamento,
                          t.m_pro_cau_rim
                   from commesse t, edificio e, regrdl re, rdl r
                  where e.commessa = t.oid
                    and re.oid = r.regrdl
                    and r.edificio = e.oid
                    and r.regrdl = NewRegRdL
                    --and r.categoria = 4 --manutenzione a guasto

                 ) loop
        if nn.m_datafermo > 0 then
          insert into autorizzazioniregrdl
            (oid, regrdl, tipoautorizzazioni)
          values
            ("sq_AUTORIZZAZIONIREGRDL".nextval, NewRegRdL, 0);
          commit;
        end if;
        if nn.m_datariavvio > 0 then
          insert into autorizzazioniregrdl
            (oid, regrdl, tipoautorizzazioni)
          values
            ("sq_AUTORIZZAZIONIREGRDL".nextval, NewRegRdL, 1);
          commit;
        end if;
        if nn.m_data_sopralluogo > 0 then
          insert into autorizzazioniregrdl
            (oid, regrdl, tipoautorizzazioni)
          values
            ("sq_AUTORIZZAZIONIREGRDL".nextval, NewRegRdL, 2);
          commit;
        end if;
        if nn.m_data_azioni_tampone > 0 then
          insert into autorizzazioniregrdl
            (oid, regrdl, tipoautorizzazioni)
          values
            ("sq_AUTORIZZAZIONIREGRDL".nextval, NewRegRdL, 3);
          commit;
        end if;
        if nn.m_data_inizio_lavori > 0 then
          insert into autorizzazioniregrdl
            (oid, regrdl, tipoautorizzazioni)
          values
            ("sq_AUTORIZZAZIONIREGRDL".nextval, NewRegRdL, 4);
          commit;
        end if;
        if nn.m_datacompletamento > 0 then
          insert into autorizzazioniregrdl
            (oid, regrdl, tipoautorizzazioni)
          values
            ("sq_AUTORIZZAZIONIREGRDL".nextval, NewRegRdL, 5);
          commit;
        end if;
        if nn.m_pro_cau_rim > 0 then
          insert into autorizzazioniregrdl
            (oid, regrdl, tipoautorizzazioni)
          values
            ("sq_AUTORIZZAZIONIREGRDL".nextval, NewRegRdL, 6);
          commit;
        end if;
      end loop;

      --end if;
    EXCEPTION
      WHEN OTHERS THEN
        oMessaggio := 'Errore imprevisto in PK_MPASSCARICOCAPACITA.CreaRegRdLbyRdL : ' ||
                      SQLERRM;

    END;

  end CreaRegRdLbyRdL;




  PROCEDURE RdlSospesa(iOidRdl IN number, oMessaggio out VARCHAR2)

   IS
    v_regrdl            number;
    V_maxregsmistamento number;
    V_maxregoperativo   number;
    v_dateoperazione    date;
    v_msgupdprec        varchar2(100) := '';
    --v_geolat number;
  begin
    oMessaggio          := '';
    v_regrdl            := 0;
    V_maxregsmistamento := 0;
    V_maxregoperativo   := 0;
    v_dateoperazione    := sysdate;

    update rdl
       set rdl.statosmistamento = 6, rdl.statooperativo = 12
     where rdl.oid = iOidRdl;
    commit;

    select nvl(rdl.regrdl, 0)
      into v_regrdl
      from rdl
     where rdl.oid = iOidRdl;
    if v_regrdl <> 0 then
      update regrdl
         set regrdl.ultimostatosmistamento  = 6,
             regrdl.ultimostatooperativo    = 12,
             regrdl.ultimadataaggiornamento = v_dateoperazione,
             regrdl.notecompletamento       = null,
             regrdl.datacompletamento       = null,
             regrdl.datafermo               = null,
             regrdl.datariavvio             = null /*,
                                                             regrdl.descrizione='Sospesa'*/
       where regrdl.oid = v_regrdl;
      commit;

      select max(r.oid)
        into V_maxregsmistamento
        from regsmistamentodett r
       where r.regrdl = v_regrdl;

      insert into regsmistamentodett
        (oid, descrizione, statosmistamento, dataora, regrdl)
      values
        ("sq_REGSMISTAMENTODETT".nextval,
         'SOSPESA RDL  ' || TO_CHAR(iOidRdl),
         6,
         v_dateoperazione,
         v_regrdl);
      commit;
      UPDDELTATREGSMPREC(V_maxregsmistamento,
                         v_dateoperazione,
                         v_msgupdprec);
      -------------------REGISTRAZIONE STATO OPERATIVO

      select count(r.oid)
        into V_maxregoperativo
        from regoperativodettaglio r
       where r.regrdl = v_regrdl;

      if V_maxregoperativo > 0 then
        select max(r.oid)
          into V_maxregoperativo
          from regoperativodettaglio r
         where r.regrdl = v_regrdl;
        UPDDELTATREGSOPREC(V_maxregoperativo,
                           v_dateoperazione,
                           v_msgupdprec);
      end if;

      insert into regoperativodettaglio
        (oid, descrizione, statooperativo, dataora)
      values
        ("sq_REGOPERATIVODETTAGLIO".nextval,
         'SOSPESA DA SO RDL  ' || TO_CHAR(iOidRdl),
         12,
         v_dateoperazione);
      commit;

    end if;

  EXCEPTION
    WHEN OTHERS THEN
      oMessaggio := 'Fallito: ' || SQLERRM;
      RAISE;

  END RdlSospesa;

  PROCEDURE RegrdlSospesa(iOidRegRdl IN number, oMessaggio out VARCHAR2)

   IS
    v_msgupdprec        varchar2(100) := '';
    V_maxregsmistamento number;
    V_maxregoperativo   number;
    v_dateoperazione    date;
    --v_geolat number;
  begin
    oMessaggio          := '';
    V_maxregsmistamento := 0;
    V_maxregoperativo   := 0;
    v_dateoperazione    := sysdate;

    update regrdl
       set regrdl.ultimostatosmistamento = 6,
           regrdl.ultimostatooperativo   = 12
     where regrdl.oid = iOidRegRdl;
    commit;
    /* select nvl(regrdl.geolat,0) into v_geolat   from regrdl
    where regrdl.oid=v_regrdl;*/

    select max(r.oid)
      into V_maxregsmistamento
      from regsmistamentodett r
     where r.regrdl = iOidRegRdl;

    insert into regsmistamentodett
      (oid, descrizione, statosmistamento, dataora, regrdl)
    values
      ("sq_REGSMISTAMENTODETT".nextval,
       'SOSPESA REGRDL  ' || TO_CHAR(iOidRegRdl),
       6,
       v_dateoperazione,
       iOidRegRdl);
    commit;
    UPDDELTATREGSMPREC(V_maxregsmistamento, v_dateoperazione, v_msgupdprec);

    select count(r.oid)
      into V_maxregoperativo
      from regoperativodettaglio r
     where r.regrdl = iOidRegRdl;

    if V_maxregoperativo > 0 then
      select max(r.oid)
        into V_maxregoperativo
        from regoperativodettaglio r
       where r.regrdl = iOidRegRdl;
      UPDDELTATREGSOPREC(V_maxregoperativo, v_dateoperazione, v_msgupdprec);
    end if;
    /*select max(r.oid)
     into V_maxregoperativo
     from regoperativodettaglio r
    where r.regrdl = iOidRegRdl;*/

    insert into regoperativodettaglio
      (oid, descrizione, statooperativo, dataora, regrdl)
    values
      ("sq_REGOPERATIVODETTAGLIO".nextval,
       'SOSPESA DA SO REGRDL  ' || TO_CHAR(iOidRegRdl),
       12,
       v_dateoperazione,
       iOidRegRdl);
    commit;
    --UPDDELTATREGSOPREC(V_maxregoperativo, v_dateoperazione, v_msgupdprec);
    /*for nr in
    (
    select rdl.oid as oidrdl from rdl where rdl.regrdl=iOidRegRdl
    )
        loop
              update rdl
              set rdl.statosmistamento=6,
                   rdl.statooperativo=12,
                   rdl.regsmistamento=V_maxregsmistamento,
                   rdl.regoperativo=V_maxregoperativo
               where rdl.oid=nr.oidrdl;
               commit;
       end loop;*/

  EXCEPTION
    WHEN OTHERS THEN
      oMessaggio := 'Fallito: ' || SQLERRM;
      RAISE;

  END RegrdlSospesa;

  PROCEDURE RdlAnnullata(iOidRdl IN number, oMessaggio out VARCHAR2)

   IS
    v_regrdl            number;
    V_maxregsmistamento number;
    V_maxregoperativo   number;
    v_dateoperazione    date;
    v_msgupdprec        varchar2(100) := '';
    --v_geolat number;
  begin
    oMessaggio          := '';
    v_regrdl            := 0;
    V_maxregsmistamento := 0;
    V_maxregoperativo   := 0;
    v_dateoperazione    := sysdate;
    update rdl
       set rdl.statosmistamento = 7, rdl.statooperativo = 13
     where rdl.oid = iOidRdl;
    commit;

    select nvl(rdl.regrdl, 0)
      into v_regrdl
      from rdl
     where rdl.oid = iOidRdl;
    if v_regrdl <> 0 then
      update regrdl
         set regrdl.ultimostatosmistamento  = 7,
             regrdl.ultimostatooperativo    = 13,
             regrdl.ultimadataaggiornamento = v_dateoperazione,
             regrdl.notecompletamento       = null,
             regrdl.datacompletamento       = null,
             regrdl.datafermo               = null,
             regrdl.datariavvio             = null /*,
                                                             regrdl.descrizione='Annullato'*/
       where regrdl.oid = v_regrdl;
      commit;
      /* select nvl(regrdl.geolat,0) into v_geolat   from regrdl
      where regrdl.oid=v_regrdl;*/

      select max(r.oid)
        into V_maxregsmistamento
        from regsmistamentodett r
       where r.regrdl = v_regrdl;

      insert into regsmistamentodett
        (oid, descrizione, statosmistamento, dataora, regrdl)
      values
        ("sq_REGSMISTAMENTODETT".nextval,
         'ANNULLATO RDL  ' || TO_CHAR(iOidRdl),
         7,
         v_dateoperazione,
         v_regrdl);
      commit;

      UPDDELTATREGSMPREC(V_maxregsmistamento,
                         v_dateoperazione,
                         v_msgupdprec);
      -------------------REGISTRAZIONE STATO OPERATIVO
      /* select max(r.oid)
       into V_maxregoperativo
       from regoperativodettaglio r
      where r.regrdl = v_regrdl;*/

      insert into regoperativodettaglio
        (oid, descrizione, statooperativo, dataora)
      values
        ("sq_REGOPERATIVODETTAGLIO".nextval,
         'ANNULLATO RDL  ' || TO_CHAR(iOidRdl),
         13,
         v_dateoperazione);
      commit;

      --UPDDELTATREGSOPREC(V_maxregoperativo, v_dateoperazione, v_msgupdprec);

    end if;

  EXCEPTION
    WHEN OTHERS THEN
      oMessaggio := 'Fallito: ' || SQLERRM;
      RAISE;

  END RdlAnnullata;

  PROCEDURE RegrdlAnnullata(iOidRegRdl IN number, oMessaggio out VARCHAR2)

   IS

    V_maxregsmistamento number;
    V_maxregoperativo   number;
    v_dateoperazione    date;
    v_msgupdprec        varchar2(100) := '';
    --v_geolat number;
  begin
    oMessaggio          := '';
    V_maxregsmistamento := 0;
    V_maxregoperativo   := 0;
    v_dateoperazione    := sysdate;
    update regrdl
       set regrdl.ultimostatosmistamento  = 7,
           regrdl.ultimostatooperativo    = 13,
           regrdl.ultimadataaggiornamento = v_dateoperazione
     where regrdl.oid = iOidRegRdl;
    commit;
    /* select nvl(regrdl.geolat,0) into v_geolat   from regrdl
    where regrdl.oid=v_regrdl;*/

    select max(r.oid)
      into V_maxregsmistamento
      from regsmistamentodett r
     where r.regrdl = iOidRegRdl;

    insert into regsmistamentodett
      (oid, descrizione, statosmistamento, dataora, regrdl)
    values
      ("sq_REGSMISTAMENTODETT".nextval,
       'ANNULLATO REGRDL  ' || TO_CHAR(iOidRegRdl),
       7,
       v_dateoperazione,
       iOidRegRdl);
    commit;

    UPDDELTATREGSMPREC(V_maxregsmistamento, v_dateoperazione, v_msgupdprec);

    /* select max(r.oid)
        into V_maxregoperativo
        from regoperativodettaglio r
       where r.regrdl = iOidRegRdl;
    */
    insert into regoperativodettaglio
      (oid, descrizione, statooperativo, dataora, regrdl)
    values
      ("sq_REGOPERATIVODETTAGLIO".nextval,
       'ANNULLATO REGRDL  ' || TO_CHAR(iOidRegRdl),
       13,
       v_dateoperazione,
       iOidRegRdl);
    commit;

    /*UPDDELTATREGSOPREC(V_maxregoperativo, v_dateoperazione, v_msgupdprec);*/

    /* for nr in
        (
        select rdl.oid as oidrdl from rdl where rdl.regrdl=iOidRegRdl
        )
            loop
                  update rdl
                  set rdl.statosmistamento=7,
                       rdl.statooperativo=13,
                       rdl.regsmistamento=V_maxregsmistamento,
                       rdl.regoperativo=V_maxregoperativo
                   where rdl.oid=nr.oidrdl;
                   commit;
           end loop;
    */

  EXCEPTION
    WHEN OTHERS THEN
      oMessaggio := 'Fallito: ' || SQLERRM;
      RAISE;

  END RegrdlAnnullata;

  PROCEDURE RegRdLEmergenzadaCO(iOidRegRdl IN number,
                                iTipo      IN varchar2,
                                oMessaggio out VARCHAR2)

   IS

    V_maxregsmistamento number;
    V_maxregoperativo   number;
    v_dateoperazione    date;
    v_msgupdprec        varchar2(100) := '';
    --v_geolat number;
  begin
    oMessaggio          := '';
    V_maxregsmistamento := 0;
    V_maxregoperativo   := 0;
    v_dateoperazione    := sysdate;

    -----------------
    insert into tbl_sql
      (s_sql, data)
    values
      ('iTipo: ' || iTipo || ', ' || iOidRegRdl, sysdate);
    commit;
    --In Emergenza da Assegnare
    update regrdl
       set regrdl.ultimostatosmistamento = 10
     where regrdl.oid = iOidRegRdl;
    commit;

    update notificheemergenzereg n
       set n.regrdl = iOidRegRdl
     where n.rdl in (select rdl.oid from rdl where rdl.regrdl = iOidRegRdl);

    commit;

    /* select nvl(regrdl.geolat,0) into v_geolat   from regrdl
    where regrdl.oid=v_regrdl;*/

    select max(r.oid)
      into V_maxregsmistamento
      from regsmistamentodett r
     where r.regrdl = iOidRegRdl;

    insert into regsmistamentodett
      (oid, descrizione, statosmistamento, dataora, regrdl)
    values
      ("sq_REGSMISTAMENTODETT".nextval,
       'In Emergenza da Assegnare' --|| TO_CHAR(iOidRegRdl)
       ,
       10,
       v_dateoperazione,
       iOidRegRdl);
    commit;
    UPDDELTATREGSMPREC(V_maxregsmistamento, v_dateoperazione, v_msgupdprec);

    -------------------REGISTRAZIONE STATO OPERATIVO

    /*  insert into regoperativodettaglio
      (oid,
      descrizione,
      ultimostatooperativo, dataora)
    values
      ("sq_REGOPERATIVODETTAGLIO".nextval,
       'SOSPESA DA SO REGRDL  '||TO_CHAR(iOidRegRdl),
        12, v_dateoperazione);
      commit;*/

  EXCEPTION
    WHEN OTHERS THEN
      oMessaggio := 'Fallito: ' || SQLERRM;
      RAISE;

  END RegRdLEmergenzadaCO;

  /*  PROCEDURE RegRdLEmergenzadaCO(iOidRegRdl IN number,
                                oMessaggio out VARCHAR2)

   IS

    V_maxregsmistamento number;
    V_maxregoperativo   number;
    v_dateoperazione    date;
    v_msgupdprec        varchar2(100) := '';
    --v_geolat number;
  begin
    oMessaggio          := '';
    V_maxregsmistamento := 0;
    V_maxregoperativo   := 0;
    v_dateoperazione    := sysdate;

    --In Emergenza da Assegnare
    update regrdl
       set regrdl.ultimostatosmistamento = 10
     where regrdl.oid = iOidRegRdl;
    commit;
    \* select nvl(regrdl.geolat,0) into v_geolat   from regrdl
    where regrdl.oid=v_regrdl;*\

    select max(r.oid)
      into V_maxregsmistamento
      from regsmistamentodett r
     where r.regrdl = iOidRegRdl;

    insert into regsmistamentodett
      (oid, descrizione, statosmistamento, dataora, regrdl)
    values
      ("sq_REGSMISTAMENTODETT".nextval,
       'In Emergenza da Assegnare REGRDL  ' || TO_CHAR(iOidRegRdl),
       10,
       v_dateoperazione,
       iOidRegRdl);
    commit;
    UPDDELTATREGSMPREC(V_maxregsmistamento, v_dateoperazione, v_msgupdprec);

    -------------------REGISTRAZIONE STATO OPERATIVO

    \*  insert into regoperativodettaglio
      (oid,
      descrizione,
      ultimostatooperativo, dataora)
    values
      ("sq_REGOPERATIVODETTAGLIO".nextval,
       'SOSPESA DA SO REGRDL  '||TO_CHAR(iOidRegRdl),
        12, v_dateoperazione);
      commit;*\

  EXCEPTION
    WHEN OTHERS THEN
      oMessaggio := 'Fallito: ' || SQLERRM;
      RAISE;

  END RegRdLEmergenzadaCO;*/

  PROCEDURE RdlCambioRisorsaTeam(iOidRdl         IN number,
                                 iOidRisorsaTeam in number,
                                 oMessaggio      out VARCHAR2)
  --la procedura è chiamata da Sospese SO---
   IS
    v_regrdl            number;
    v_risorsacapo       number;
    V_maxregsmistamento number;
    V_maxregoperativo   number;
    V_regcambiorisorsa  number;
    v_dateoperazione    date;
    v_msgupdprec        varchar2(100) := '';
    --v_geolat number;
  begin
    oMessaggio          := '';
    v_regrdl            := 0;
    V_maxregsmistamento := 0;
    V_maxregoperativo   := 0;
    V_regcambiorisorsa  := 0;
    v_risorsacapo       := 0;
    v_dateoperazione    := sysdate;

    insert into regcambiorisorsa
      (oid, attualeteamrisorsa, dataultimoagg)
    values
      ("sq_REGCAMBIORISORSA".nextval, iOidRisorsaTeam, v_dateoperazione);
    commit;
    SELECT MAX(oid) INTO V_regcambiorisorsa from regcambiorisorsa;

    select s.risorsacapo
      into v_risorsacapo
      from risorseteam s
     where s.oid = iOidRisorsaTeam;

    insert into regcambiorisorsadett
      (oid, regcambiorisorsa, trisorsa, dataora)
    values
      ("sq_REGCAMBIORISORSADETT".nextval,
       V_regcambiorisorsa,
       v_risorsacapo,
       v_dateoperazione);
    commit;

    update rdl
       set rdl.statosmistamento = 2,
           rdl.statooperativo   = 19,
           rdl.risorsateam      = iOidRisorsaTeam,
           rdl.regcambiorisorsa = V_regcambiorisorsa
     where rdl.oid = iOidRdl;

    select nvl(rdl.regrdl, 0)
      into v_regrdl
      from rdl
     where rdl.oid = iOidRdl;
    if v_regrdl <> 0 then

      update regrdl
         set regrdl.ultimostatosmistamento  = 2,
             regrdl.ultimostatooperativo    = 19,
             regrdl.ultimadataaggiornamento = v_dateoperazione
       where regrdl.oid = v_regrdl;
      commit;

      select max(r.oid)
        into V_maxregsmistamento
        from regsmistamentodett r
       where r.regrdl = v_regrdl;

      insert into regsmistamentodett
        (oid, descrizione, statosmistamento, dataora, regrdl)
      values
        ("sq_REGSMISTAMENTODETT".nextval,
         'Assegnata ad altra risorsa RegRdL ' || TO_CHAR(v_regrdl),
         2,
         v_dateoperazione,
         v_regrdl);
      commit;

      UPDDELTATREGSMPREC(V_maxregsmistamento,
                         v_dateoperazione,
                         v_msgupdprec);

      select count(r.oid)
        into V_maxregoperativo
        from regoperativodettaglio r
       where r.regrdl = v_regrdl;

      if V_maxregoperativo > 0 then
        select max(r.oid)
          into V_maxregoperativo
          from regoperativodettaglio r
         where r.regrdl = v_regrdl;
        UPDDELTATREGSOPREC(V_maxregoperativo,
                           v_dateoperazione,
                           v_msgupdprec);
      end if;

      insert into regoperativodettaglio
        (oid, descrizione, statooperativo, dataora)
      values
        ("sq_REGOPERATIVODETTAGLIO".nextval,
         'Da prendere in carico ad altra risorsa RegRdL ' ||
         TO_CHAR(v_regrdl),
         19,
         v_dateoperazione);
      commit;

    end if;

  EXCEPTION
    WHEN OTHERS THEN
      oMessaggio := 'Fallito: ' || SQLERRM;
      RAISE;

  END RdlCambioRisorsaTeam;

  PROCEDURE RegrdlCambioRisorsaTeam(iOidRegRdl      IN number,
                                    iOidRisorsaTeam in number,
                                    oMessaggio      out VARCHAR2)

   IS

    v_risorsacapo       number;
    V_maxregsmistamento number;
    V_maxregoperativo   number;
    V_regcambiorisorsa  number;
    v_dateoperazione    date;
    v_msgupdprec        varchar2(100) := '';

  begin

    oMessaggio          := '';
    v_risorsacapo       := 0;
    V_maxregsmistamento := 0;
    V_maxregoperativo   := 0;
    V_regcambiorisorsa  := 0;
    v_dateoperazione    := sysdate;

    select s.risorsacapo
      into v_risorsacapo
      from risorseteam s
     where s.oid = iOidRisorsaTeam;

    update regrdl
       set regrdl.ultimostatosmistamento  = 2,
           regrdl.ultimostatooperativo    = 19,
           regrdl.ultimadataaggiornamento = v_dateoperazione
     where regrdl.oid = iOidRegRdl;
    commit;

    select max(r.oid)
      into V_maxregsmistamento
      from regsmistamentodett r
     where r.regrdl = iOidRegRdl;

    insert into regsmistamentodett
      (oid, descrizione, statosmistamento, dataora, regrdl)
    values
      ("sq_REGSMISTAMENTODETT".nextval,
       'ASSEGNATA REGRDL  ' || TO_CHAR(iOidRegRdl),
       2,
       v_dateoperazione,
       iOidRegRdl);
    commit;
    UPDDELTATREGSMPREC(V_maxregsmistamento, v_dateoperazione, v_msgupdprec);
    select max(r.oid)
      into V_maxregoperativo
      from regoperativodettaglio r
     where r.regrdl = iOidRegRdl;

    insert into regoperativodettaglio
      (oid, descrizione, statooperativo, dataora, regrdl)
    values
      ("sq_REGOPERATIVODETTAGLIO".nextval,
       'Da prendere in carico REGRDL  ' || TO_CHAR(iOidRegRdl),
       19,
       v_dateoperazione,
       iOidRegRdl);
    commit;

    UPDDELTATREGSOPREC(V_maxregoperativo, v_dateoperazione, v_msgupdprec);

    for nr in (select rdl.oid as oidrdl
                 from rdl
                where rdl.regrdl = iOidRegRdl) loop

      insert into regcambiorisorsa
        (oid, attualeteamrisorsa, dataultimoagg)
      values
        ("sq_REGCAMBIORISORSA".nextval, iOidRisorsaTeam, v_dateoperazione);
      commit;
      SELECT MAX(oid) INTO V_regcambiorisorsa from regcambiorisorsa;

      insert into regcambiorisorsadett
        (oid, regcambiorisorsa, trisorsa, dataora)
      values
        ("sq_REGCAMBIORISORSADETT".nextval,
         V_regcambiorisorsa,
         v_risorsacapo,
         v_dateoperazione);
      commit;

      update rdl
         set rdl.statosmistamento = 2,
             rdl.statooperativo   = 19,
             --rdl.regsmistamento=V_maxregsmistamento,
             --rdl.regoperativo=V_maxregoperativo,
             rdl.risorsateam      = iOidRisorsaTeam,
             rdl.regcambiorisorsa = V_regcambiorisorsa
       where rdl.oid = nr.oidrdl;
      commit;
    end loop;

  EXCEPTION
    WHEN OTHERS THEN
      oMessaggio := 'Fallito: ' || SQLERRM;
      RAISE;

  END RegrdlCambioRisorsaTeam;

  PROCEDURE RegRdlAssegnaRisorsaTeam(iOidRegRdl      IN number,
                                     iOidRisorsaTeam in number,
                                     oMessaggio      out VARCHAR2)

   IS

    v_risorsacapo       number;
    v_oidodlnextval     number := 0;
    V_maxregsmistamento number;
    V_maxregoperativo   number;
    V_regcambiorisorsa  number;
    v_dateoperazione    date;
    v_msgupdprec        varchar2(100) := '';
    v_countrdlregrdl    number := 0;
    v_impianto          number := 0;
    v_TOTRISORSE        number := 0;

  begin


    oMessaggio          := '';
    v_risorsacapo       := 0;
    V_maxregsmistamento := 0;
    V_maxregoperativo   := 0;
    V_regcambiorisorsa  := 0;
    v_dateoperazione    := sysdate;


    insert into tbl_sql
  (s_sql, data)
values
(
'RegRdlAssegnaRisorsaTeam---'|| CHR(13) || CHR(10)||
 'iOidRegRdl:'||iOidRegRdl||' ,'||CHR(13) || CHR(10)||
 'iOidRisorsaTeam:'||iOidRisorsaTeam||' ,'||CHR(13) || CHR(10)
  ,sysdate);
  --(v_categoria ||','||v_SSmistamento||','||v_SOperativo, sysdate);
    commit;

    select s.risorsacapo
      into v_risorsacapo
      from risorseteam s
     where s.oid = iOidRisorsaTeam;

    select count(ar.oid)
      into v_TOTRISORSE
      from assrisorseteam ar
     where ar.risorseteam = iOidRisorsaTeam;

    update regrdl
       set regrdl.ultimostatosmistamento  = 2,
           regrdl.ultimostatooperativo    = 19,
           regrdl.ultimadataaggiornamento = v_dateoperazione,
           regrdl.dataassegnazioneodl     = v_dateoperazione /*,
                                           regrdl.descrizione='Assegnata'           */
     where regrdl.oid = iOidRegRdl;
    commit;

    select "sq_ODL".Nextval into v_oidodlnextval from dual;

    select count(r.oid)
      into v_countrdlregrdl
      from rdl r
     where r.regrdl = iOidRegRdl;

    select max(r.impianto)
      into v_impianto
      from rdl r
     where r.regrdl = iOidRegRdl;

    insert into odl
      (OID, --
       DESCRIPTION,
       QTYOPENRDL,
       TOTRISORSE,
       REGRDL,
       TIPOODL,
       STATOOPERATIVO,
       DATAEMISSIONE)
    values
      (v_oidodlnextval, --OID
       'OdL TT(' || to_char(iOidRegRdl) || '-' || to_char(v_oidodlnextval) ||
       ' Impianto ' || (select impianto.descrizione
                          from impianto
                         where impianto.oid = v_impianto),
       v_countrdlregrdl,
       v_TOTRISORSE,
       iOidRegRdl,
       5,
       19,
       v_dateoperazione);
    commit;

    select max(r.oid)
      into V_maxregsmistamento
      from regsmistamentodett r
     where r.regrdl = iOidRegRdl;

    insert into regsmistamentodett
      (oid, descrizione, statosmistamento, dataora, regrdl)
    values
      ("sq_REGSMISTAMENTODETT".nextval,
       'Assegnata RegRdL nr' || TO_CHAR(iOidRegRdl),
       2,
       v_dateoperazione,
       iOidRegRdl);
    commit;
    UPDDELTATREGSMPREC(V_maxregsmistamento, v_dateoperazione, v_msgupdprec);
    --ok---
    /* select max(r.oid)
     into V_maxregoperativo
     from regoperativodettaglio r
    where r.regrdl = iOidRegRdl;*/

    insert into regoperativodettaglio
      (oid, descrizione, statooperativo, dataora, regrdl)
    values
      ("sq_REGOPERATIVODETTAGLIO".nextval,
       'Assegnata-Da prendere in carico RegRdL nr' || TO_CHAR(iOidRegRdl),
       19,
       v_dateoperazione,
       iOidRegRdl);
    commit;

    -- UPDDELTATREGSOPREC(V_maxregoperativo, v_dateoperazione, v_msgupdprec);

    for nr in (select rdl.oid as oidrdl
                 from rdl
                where rdl.regrdl = iOidRegRdl) loop

      update rdl
         set --rdl.statosmistamento = 2,
             --  rdl.statooperativo   = 19,
                              rdl.odl = v_oidodlnextval,
             rdl.dataassegnazioneodl = v_dateoperazione --,
      --rdl.regsmistamento=V_maxregsmistamento,
      --rdl.regoperativo=V_maxregoperativo,
      --  rdl.risorsateam      = iOidRisorsaTeam--,
      ---rdl.regcambiorisorsa = V_regcambiorisorsa
       where rdl.oid = nr.oidrdl;
      commit;
    end loop;

  EXCEPTION
    WHEN OTHERS THEN
      oMessaggio := 'Fallito: ' || SQLERRM;
      RAISE;

  END RegRdlAssegnaRisorsaTeam;

  PROCEDURE RdlMigrazionepmptt(iOidRdl IN number, oMessaggio out VARCHAR2)

   IS
    v_regrdl            number;
    V_maxregsmistamento number;
    V_maxregoperativo   number;
    v_dateoperazione    date;
    v_msgupdprec        varchar2(100) := '';
    --v_geolat number;
  begin
    oMessaggio          := '';
    v_regrdl            := 0;
    V_maxregsmistamento := 0;
    V_maxregoperativo   := 0;
    v_dateoperazione    := sysdate;

    update rdl
       set rdl.statosmistamento = 2,
           rdl.statooperativo   = 19,
           rdl.categoria        = 2
     where rdl.oid = iOidRdl;

    select nvl(rdl.regrdl, 0)
      into v_regrdl
      from rdl
     where rdl.oid = iOidRdl;
    if v_regrdl <> 0 then

      update regrdl
         set regrdl.ultimostatosmistamento  = 2,
             regrdl.ultimostatooperativo    = 19,
             regrdl.ultimadataaggiornamento = v_dateoperazione
       where regrdl.oid = v_regrdl;
      commit;
      /* select nvl(regrdl.geolat,0) into v_geolat   from regrdl
      where regrdl.oid=v_regrdl;*/

      select max(r.oid)
        into V_maxregsmistamento
        from regsmistamentodett r
       where r.regrdl = v_regrdl;

      insert into regsmistamentodett
        (oid, descrizione, statosmistamento, dataora, regrdl)
      values
        ("sq_REGSMISTAMENTODETT".nextval,
         'ASSEGNATA RDL ' || TO_CHAR(iOidRdl),
         2,
         v_dateoperazione,
         v_regrdl);
      commit;
      UPDDELTATREGSMPREC(V_maxregsmistamento,
                         v_dateoperazione,
                         v_msgupdprec);

      select count(r.oid)
        into V_maxregoperativo
        from regoperativodettaglio r
       where r.regrdl = v_regrdl;

      if V_maxregoperativo > 0 then
        select max(r.oid)
          into V_maxregoperativo
          from regoperativodettaglio r
         where r.regrdl = v_regrdl;
        UPDDELTATREGSOPREC(V_maxregoperativo,
                           v_dateoperazione,
                           v_msgupdprec);
      end if;

      insert into regoperativodettaglio
        (oid, descrizione, statooperativo, dataora)
      values
        ("sq_REGOPERATIVODETTAGLIO".nextval,
         'Da prendere in carico RDL ' || TO_CHAR(iOidRdl),
         19,
         v_dateoperazione);
      commit;

    end if;

  EXCEPTION
    WHEN OTHERS THEN
      oMessaggio := 'Fallito: ' || SQLERRM;
      RAISE;

  END RdlMigrazionepmptt;

  PROCEDURE RegrdlMigrazionepmpt(iOidRegRdl IN number,
                                 ouser      in varchar2,
                                 oMessaggio out VARCHAR2)

   IS

    v_newregrdl         number;
    V_maxregsmistamento number := 0;
    V_maxregoperativo   number := 0;
    v_dateoperazione    date;
    v_msgupdprec        varchar2(100) := '';
    v_stsm_pre          number := 0;
    v_stop_pre          number := 0;
    --v_geolat number;
  begin

    oMessaggio          := '';
    V_maxregsmistamento := 0;
    V_maxregoperativo   := 0;
    v_dateoperazione    := sysdate;

    -- sospensione regrdl ---

    update regrdl
       set regrdl.ultimostatosmistamento = 6,
           regrdl.ultimostatooperativo   = 12,
           regrdl.utente                 = ouser,
           regrdl.dataupdate             = v_dateoperazione
     where regrdl.oid = iOidRegRdl;
    commit;

    for nr in (select rdl.oid as oidrdl
                 from rdl
                where rdl.regrdl = iOidRegRdl) loop
      update rdl
         set rdl.statosmistamento  = 6,
             rdl.statooperativo    = 12,
             rdl.utenteinserimento = ouser,
             rdl.dataupdate        = v_dateoperazione
       where rdl.oid = nr.oidrdl;
      commit;
    end loop;

    select max(r.oid)
      into V_maxregsmistamento
      from regsmistamentodett r
     where r.regrdl = iOidRegRdl;

    select r.statosmistamento
      into v_stsm_pre
      from regsmistamentodett r
     where r.oid = V_maxregsmistamento;

    UPDDELTATREGSMPREC(V_maxregsmistamento, v_dateoperazione, v_msgupdprec);

    insert into regsmistamentodett
      (oid, descrizione, statosmistamento, dataora, regrdl)
    values
      ("sq_REGSMISTAMENTODETT".nextval,
       'RegRdL nr ' || iOidRegRdl || ' Variazione Stato Smistamento da ' ||
       (select sm.statosmistamento
          from statosmistamento sm
         where sm.oid = v_stsm_pre) || ' a ' ||
       (select sm.statosmistamento from statosmistamento sm where sm.oid = 6),
       6,
       v_dateoperazione,
       iOidRegRdl);
    commit;

    select max(r.oid)
      into V_maxregoperativo
      from regoperativodettaglio r
     where r.regrdl = iOidRegRdl;

    select r.statooperativo
      into v_stop_pre
      from regoperativodettaglio r
     where r.regrdl = V_maxregoperativo;
    UPDDELTATREGSOPREC(V_maxregoperativo, v_dateoperazione, v_msgupdprec);
    insert into regoperativodettaglio
      (oid, descrizione, statooperativo, dataora, regrdl)
    values
      ("sq_REGOPERATIVODETTAGLIO".nextval,
       'RegRdL nr ' || iOidRegRdl || 'Variazione Stato Operativo da ' ||
       (select so.statooperativo
          from statooperativo so
         where so.oid = v_stop_pre) || ' a ' ||
       (select so.statooperativo from statooperativo so where so.oid = 12),
       12,
       v_dateoperazione,
       iOidRegRdl);
    commit;

    for nn in (select rr.apparato,
                      rr.datafermo,
                      rr.datariavvio,
                      rr.descrizione,
                      rr.priorita,
                      rr.risorsateam,
                      rr.utente
                 from regrdl rr
                where rr.oid = iOidRegRdl) loop

      select "sq_REGRDL".Nextval into v_newregrdl from dual;
      insert into regrdl
        (oid,
         descrizione,
         utente,
         data_creazione_rdl,
         ultimostatooperativo,
         ultimostatosmistamento,
         risorsateam,
         priorita,
         apparato,
         categoria)
      values
        (v_newregrdl,
         nn.descrizione,
         ouser,
         v_dateoperazione,
         19,
         2,
         nn.risorsateam,
         nn.priorita,
         nn.apparato,
         5);
      commit;
      for nnn in (select rr.apparato,
                         rr.descrizione,
                         rr.edificio,
                         rr.sistema,
                         rr.richiedente,
                         rr.risorsateam,
                         rr.priorita,
                         rr.tipointervento,
                         rr.impianto,
                         rr.utenteinserimento
                    from rdl rr
                   where rr.regrdl = iOidRegRdl) loop
        insert into rdl
          (oid,
           richiedente,
           sistema,
           edificio,
           impianto,
           apparato,
           priorita,
           categoria,
           tipointervento,
           descrizione,
           datacreazione,
           datarichiesta,
           utenteinserimento,
           risorsateam,
           statosmistamento,
           statooperativo,
           regrdl)
        values
          ("sq_RDL".Nextval,
           nnn.richiedente,
           nnn.sistema,
           nnn.edificio,
           nnn.impianto,
           nnn.apparato,
           nn.priorita,
           5,
           nnn.tipointervento,
           nnn.descrizione,
           v_dateoperazione,
           v_dateoperazione,
           ouser,
           nnn.risorsateam,
           2,
           19,
           v_newregrdl);
        commit;
      end loop;

    end loop;
    -------

    insert into regsmistamentodett
      (oid, descrizione, statosmistamento, dataora, regrdl)
    values
      ("sq_REGSMISTAMENTODETT".nextval,
       'RegRdL nr ' || v_newregrdl || ' Stato Smistamento  ' ||
       (select sm.statosmistamento from statosmistamento sm where sm.oid = 2),
       2,
       v_dateoperazione,
       iOidRegRdl);
    commit;

    insert into regoperativodettaglio
      (oid, descrizione, statooperativo, dataora, regrdl)
    values
      ("sq_REGOPERATIVODETTAGLIO".nextval,
       'RegRdL nr ' || v_newregrdl || 'Stato Operativo ' ||
       (select so.statooperativo from statooperativo so where so.oid = 19),
       19,
       v_dateoperazione,
       iOidRegRdl);
    commit;

  EXCEPTION
    WHEN OTHERS THEN
      oMessaggio := 'Fallito: ' || SQLERRM;
      RAISE;

  END RegrdlMigrazionepmpt;

  /*  PROCEDURE RdlCompletamento(iOidRegRdl IN number,
                                 oMessaggio out VARCHAR2)

   IS
    v_regrdl            number;
    V_maxregsmistamento number;
    V_maxregoperativo   number;
    v_dateoperazione    date;
    v_msgupdprec        varchar2(100) := '';
    --v_geolat number;
  begin
    oMessaggio          := '';
    v_regrdl            := 0;
    V_maxregsmistamento := 0;
    V_maxregoperativo   := 0;
    v_dateoperazione    := sysdate;

    update rdl
       set rdl.statosmistamento  = 4,
           rdl.statooperativo    = 11,
           rdl.datacompletamento = v_dateoperazione
     where rdl.regrdl = iOidRegrdL;

    select nvl(rdl.regrdl, 0)
      into v_regrdl
      from rdl
     where rdl.regrdl = iOidRegrdL;
    if v_regrdl <> 0 then

      update regrdl
         set regrdl.ultimostatosmistamento  = 4,
             regrdl.ultimostatooperativo    = 11,
             regrdl.ultimadataaggiornamento = v_dateoperazione
       where regrdl.oid = v_regrdl;
      commit;
      \* select nvl(regrdl.geolat,0) into v_geolat   from regrdl
      where regrdl.oid=v_regrdl;*\

      select max(r.oid)
        into V_maxregsmistamento
        from regsmistamentodett r
       where r.regrdl = v_regrdl;

      insert into regsmistamentodett
        (oid, descrizione, statosmistamento, dataora, regrdl)
      values
        ("sq_REGSMISTAMENTODETT".nextval,
         'Lavorazione Conclusa RegRdL ' || TO_CHAR(iOidRegrdL),
         4,
         v_dateoperazione,
         v_regrdl);
      commit;
      UPDDELTATREGSMPREC(V_maxregsmistamento,
                         v_dateoperazione,
                         v_msgupdprec);
      select max(r.oid)
        into V_maxregoperativo
        from regoperativodettaglio r
       where r.regrdl = v_regrdl;

      insert into regoperativodettaglio
        (oid, descrizione, ultimostatooperativo, dataora,
        --regoperativodettaglio.regrdl,
        flagcompletatoso
        )
      values
        ("sq_REGOPERATIVODETTAGLIO".nextval,
         'COMPLETATO RDL ' || TO_CHAR(iOidRegrdL),
         11,v_dateoperazione,
         ---???regrdl
         1
         );
      commit;
      UPDDELTATREGSOPREC(V_maxregoperativo, v_dateoperazione, v_msgupdprec);

    end if;

  EXCEPTION
    WHEN OTHERS THEN
      oMessaggio := 'Fallito: ' || SQLERRM;
      RAISE;

  END RdlCompletamento;*/

  PROCEDURE RdlCompletamento(iOidRegRdl IN number, oMessaggio out VARCHAR2)

   IS

    V_maxregsmistamento number;
    V_maxregoperativo   number;
    v_dateoperazione    date;
    v_msgupdprec        varchar2(100) := '';
    v_pcrcausarimedio   number;
    v_datacompletamento date;
    v_datariavvio       date;
    v_datafermo         date;
    v_notecompletamento VARCHAR2(1000);

    --v_geolat number;
  begin

    oMessaggio          := '';
    V_maxregsmistamento := 0;
    V_maxregoperativo   := 0;
    v_dateoperazione    := sysdate;

    update regrdl
       set regrdl.ultimostatosmistamento  = 4,
           regrdl.ultimostatooperativo    = 11,
           regrdl.ultimadataaggiornamento = v_dateoperazione
     where regrdl.oid = iOidRegRdl;
    commit;

    select regrdl.pcrcausarimedio,
           regrdl.datacompletamento,
           regrdl.datariavvio,
           regrdl.datafermo,
           regrdl.notecompletamento
      into v_pcrcausarimedio,
           v_datacompletamento,
           v_datariavvio,
           v_datafermo,
           v_notecompletamento
      from regrdl
     where regrdl.oid = iOidRegRdl;

    /* select nvl(regrdl.geolat,0) into v_geolat   from regrdl
    where regrdl.oid=v_regrdl;*/

    select max(r.oid)
      into V_maxregsmistamento
      from regsmistamentodett r
     where r.regrdl = iOidRegRdl;

    insert into regsmistamentodett
      (oid, descrizione, statosmistamento, dataora, regrdl)
    values
      ("sq_REGSMISTAMENTODETT".nextval,
       'Lavorazione Conclusa RegRdL ' || TO_CHAR(iOidRegRdl),
       4,
       v_dateoperazione,
       iOidRegRdl);
    commit;
    UPDDELTATREGSMPREC(V_maxregsmistamento, v_dateoperazione, v_msgupdprec);

    select max(r.oid)
      into V_maxregoperativo
      from regoperativodettaglio r
     where r.regrdl = iOidRegRdl;

    insert into regoperativodettaglio
      (oid, descrizione, statooperativo, dataora, regrdl, flagcompletatoso)
    values
      ("sq_REGOPERATIVODETTAGLIO".nextval,
       'Completato da SO REGRDL ' || TO_CHAR(iOidRegRdl),
       11,
       v_dateoperazione,
       iOidRegRdl,
       1);
    commit;
    UPDDELTATREGSOPREC(V_maxregoperativo, v_dateoperazione, v_msgupdprec);

    for nr in (select rdl.oid as oidrdl,
                      rdl.datacompletamento,
                      rdl.datafermo,
                      rdl.datariavvio,
                      rdl.pcrcausarimedio,
                      rdl.notecompletamento
                 from rdl
                where rdl.regrdl = iOidRegRdl) loop
      update rdl
         set rdl.statosmistamento = 4, rdl.statooperativo = 11 --,
      --rdl.regsmistamento=V_maxregsmistamento,
      --rdl.regoperativo=V_maxregoperativo,
      --rdl.datacompletamento = v_dateoperazione
       where rdl.oid = nr.oidrdl;
      commit;
      if nr.datafermo is null then
        update rdl
           set rdl.datafermo = v_datafermo
         where rdl.oid = nr.oidrdl;
      end if;
      if nr.datariavvio is null then
        update rdl
           set rdl.datafermo = v_datariavvio
         where rdl.oid = nr.oidrdl;
      end if;
      if nr.datacompletamento is null then
        update rdl
           set rdl.datacompletamento = v_datacompletamento
         where rdl.oid = nr.oidrdl;
      end if;
      if nr.pcrcausarimedio is null then
        update rdl
           set rdl.pcrcausarimedio = v_pcrcausarimedio
         where rdl.oid = nr.oidrdl;
      end if;
      if nr.notecompletamento is null then
        update rdl
           set rdl.notecompletamento = v_notecompletamento
         where rdl.oid = nr.oidrdl;
      end if;

    end loop;

  EXCEPTION
    WHEN OTHERS THEN
      oMessaggio := 'Fallito: ' || SQLERRM;
      RAISE;

  END RdlCompletamento;

  PROCEDURE RegrdlCompletamento(iOidRegRdl IN number,
                                oMessaggio out VARCHAR2)

   IS

    V_maxregsmistamento number;
    V_maxregoperativo   number;
    v_dateoperazione    date;
    v_msgupdprec        varchar2(100) := '';
    v_pcrcausarimedio   number;
    v_datacompletamento date;
    v_datariavvio       date;
    v_datafermo         date;
    v_notecompletamento VARCHAR2(1000);
    v_categoria         number := 0;
    voMessaggio         VARCHAR2(250);

    --v_geolat number;
  begin

    oMessaggio          := '';
    V_maxregsmistamento := 0;
    V_maxregoperativo   := 0;
    v_dateoperazione    := sysdate;

    update regrdl
       set regrdl.ultimostatosmistamento  = 4,
           regrdl.ultimostatooperativo    = 11,
           regrdl.ultimadataaggiornamento = v_dateoperazione
     where regrdl.oid = iOidRegRdl;
    commit;

    select regrdl.pcrcausarimedio,
           regrdl.datacompletamento,
           regrdl.datariavvio,
           regrdl.datafermo,
           regrdl.notecompletamento,
           regrdl.categoria
      into v_pcrcausarimedio,
           v_datacompletamento,
           v_datariavvio,
           v_datafermo,
           v_notecompletamento,
           v_categoria
      from regrdl
     where regrdl.oid = iOidRegRdl;

    /* select nvl(regrdl.geolat,0) into v_geolat   from regrdl
    where regrdl.oid=v_regrdl;*/

    select max(r.oid)
      into V_maxregsmistamento
      from regsmistamentodett r
     where r.regrdl = iOidRegRdl;

    insert into regsmistamentodett
      (oid, descrizione, statosmistamento, dataora, regrdl)
    values
      ("sq_REGSMISTAMENTODETT".nextval,
       'COMPLETATO REGRDL ' || TO_CHAR(iOidRegRdl),
       4,
       v_dateoperazione,
       iOidRegRdl);
    commit;
    UPDDELTATREGSMPREC(V_maxregsmistamento, v_dateoperazione, v_msgupdprec);

    select max(r.oid)
      into V_maxregoperativo
      from regoperativodettaglio r
     where r.regrdl = iOidRegRdl;

    insert into regoperativodettaglio
      (oid, descrizione, statooperativo, dataora, regrdl, flagcompletatoso)
    values
      ("sq_REGOPERATIVODETTAGLIO".nextval,
       'COMPLETATO REGRDL ' || TO_CHAR(iOidRegRdl),
       11,
       v_dateoperazione,
       iOidRegRdl,
       1);
    commit;
    UPDDELTATREGSOPREC(V_maxregoperativo, v_dateoperazione, v_msgupdprec);
    for nr in (select rdl.oid as oidrdl,
                      rdl.datacompletamento,
                      rdl.datafermo,
                      rdl.datariavvio,
                      rdl.pcrcausarimedio,
                      rdl.notecompletamento
                 from rdl
                where rdl.regrdl = iOidRegRdl) loop
      update rdl
         set rdl.statosmistamento = 4, rdl.statooperativo = 11 --,
      --rdl.regsmistamento=V_maxregsmistamento,
      --rdl.regoperativo=V_maxregoperativo,
      --rdl.datacompletamento = v_dateoperazione
       where rdl.oid = nr.oidrdl;
      commit;
      if nr.datafermo is null then
        update rdl
           set rdl.datafermo = v_datafermo
         where rdl.oid = nr.oidrdl;
      end if;
      if nr.datariavvio is null then
        update rdl
           set rdl.datafermo = v_datariavvio
         where rdl.oid = nr.oidrdl;
      end if;
      if nr.datacompletamento is null then
        update rdl
           set rdl.datacompletamento = v_datacompletamento
         where rdl.oid = nr.oidrdl;
      end if;
      if nr.pcrcausarimedio is null then
        update rdl
           set rdl.pcrcausarimedio = v_pcrcausarimedio
         where rdl.oid = nr.oidrdl;
      end if;
      if nr.notecompletamento is null then
        update rdl
           set rdl.notecompletamento = v_notecompletamento
         where rdl.oid = nr.oidrdl;
      end if;

    end loop;
    if v_categoria = 1 then
      pk_clona_regrdl.clona_regrdl_mp(iOidRegRdl, voMessaggio);
    end if;

  EXCEPTION
    WHEN OTHERS THEN
      oMessaggio := 'Fallito: ' || SQLERRM;
      RAISE;

  END RegrdlCompletamento;

  PROCEDURE Parlinkcompletamento(oMessaggio out VARCHAR2)

   IS

  begin

    oMessaggio := '/appscams/Attivita/AvanzamentoStatoLavoriCentraleOperativa';

  EXCEPTION
    WHEN OTHERS THEN
      oMessaggio := 'Fallito: ' || SQLERRM;
      RAISE;

  END Parlinkcompletamento;

  procedure inskpimtbf(iOidREGRDL IN number, p_msg_out out VARCHAR2) is
    varmsgins        varchar2(300) := 'Insert into kpi_mtbf';
    v_err_code       varchar2(200);
    v_err_msg        varchar2(250);
    v_maxdatariavvio date;
    v_datafermo      date;
   v_maxdUpTime date; -- precedente alla DownTime
    v_DownTime      date;

    v_conta          number := 0;
  begin
    --inserimento tabella kpi_mtbf
    for nr in (
                      select rdl.oid as rdl,
                      rdl.DATARICHIESTA,
                      rdl.datacompletamento,
                      rdl.datariavvio,
                      rdl.datafermo,
                      round((nvl(rdl.datariavvio, rdl.datacompletamento) -
                            nvl(rdl.datafermo, rdl.DATARICHIESTA)) * 24 * 60) as tempomttr,
                      case
                        when (rdl.datariavvio is not null And rdl.datafermo is not null) then
                         0  -- Disservizio(0)
                        else
                         1   -- InServizio(1)
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
                  and rdl.apparato is not null
                  and rdl.apparato = a.oid
                  and a.stdapparato = st.oid
                  and st.apparatostdclassi = stc.oid
                  and a.impianto = i.oid
                  and i.edificio = e.oid
                  and co.edificio = e.oid
                  and c.oid = e.commessa
                  and ap.oid = c.areadipolo) loop

   if nr.tipomtbf = 1 then --(1=inservizio); 0 condisservizio)
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

      else  -- --- quindi date fermo e riavvio sono valorizzate
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
           set
           k.datarichiesta      = nr.datarichiesta,
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

  PROCEDURE UPDDELTATREGSMPREC(iOidREGSMDET IN number,
                               odate        in date,
                               oMessaggio   out VARCHAR2) is
    vdataprecedente date;
  begin
    oMessaggio := 'UPD ' || iOidREGSMDET;
    select r.dataora
      into vdataprecedente
      from regsmistamentodett r
     where r.oid = iOidREGSMDET;

    update regsmistamentodett r
       set r.deltatempo = round((odate - vdataprecedente) * 24 * 60)
     where r.oid = iOidREGSMDET;
    commit;

  EXCEPTION
    WHEN OTHERS THEN
      oMessaggio := 'Fallito: ' || SQLERRM;
      RAISE;
  end UPDDELTATREGSMPREC;

  PROCEDURE UPDDELTATREGSOPREC(iOidREGSODET IN number,
                               odate        in date,
                               oMessaggio   out VARCHAR2) is
    vdataprecedente date;
  begin
    oMessaggio := 'UPD ' || iOidREGSODET;

    select r.dataora
      into vdataprecedente
      from regoperativodettaglio r
     where r.oid = iOidREGSODET;

    update regoperativodettaglio r
       set r.deltatempo = round((odate - vdataprecedente) * 24 * 60)
     where r.oid = iOidREGSODET;
    commit;

  EXCEPTION
    WHEN OTHERS THEN
      oMessaggio := 'Fallito: ' || SQLERRM;
      RAISE;
  end UPDDELTATREGSOPREC;

  FUNCTION GetDistanzeCOImpianto(iOidCluster  IN number,
                                 iOidEdificio IN number,
                                 iOidImpianto IN number /*,
                                                                                                                                                                                                                                                                                                                                            oDistanza out VARCHAR2,
                                                                                                                                                                                                                                                                                                                                            oMessaggio out VARCHAR2*/
                                 --IO_CURSOR   IN OUT T_CURSOR
                                 ) Return VARCHAR2 IS

    v_geolati_str          varchar2(100);
    v_geolngi_str          varchar2(100);
    v_geolati              number;
    v_geolngi              number;
    v_geolatr              number;
    v_geolngr              number;
    v_distanza             number;
    parametro              number;
    conta_nr_attivita_sosp number;
    conta_nr_attivita_ass  number;
    v_max                  number;
    ContaIndirizzo         number;
    ContaImpianto          number;
    ContaCluster           number;
    ContaClusterImpianto   number;

    oDistanza   varchar2(100);
    v_co_geolat number;
    v_co_geolng number;
    v_i_geolat  number;
    v_i_geolng  number;
  BEGIN
    --oMessaggio  := '';
    v_co_geolat := 0;
    v_co_geolng := 0;
    v_i_geolat  := 0;
    v_i_geolng  := 0;
    --  calcolo coordinate edificio
    select i.geolat, i.geolng
      into v_i_geolat, v_i_geolng
      from edificio e, indirizzo i
     where e.indirizzo = i.oid
       and e.oid = iOidEdificio;

    -- calcolo coordinate co
    select i.geolat, i.geolng
      into v_co_geolat, v_co_geolng
      from scenario        s,
           clusteredifici  cl,
           centrooperativo co,
           edificio        e,
           indirizzo       i
     where s.oid = cl.scenario
       and s.centrooperativo = co.oid
       and co.edificio = e.oid
       and e.indirizzo = i.oid
       and cl.oid = iOidCluster;
    /*   var milano = new { lat = 45.464194D, lon = 9.188132D };
    var roma = new { lat = 41.904321D, lon = 12.491455D };*/

    -- test -->  v_geolatr:= 41.904321  ; v_geolngr :=9.188132   ;
    -- se esiste il Team allora esiste necessariamente la risorsa capo!
    /*
    v_co_geolat:= 45.464194  ; v_co_geolng :=  9.188132   ;
     v_i_geolat:=  41.904321  ; v_i_geolng := 12.491455   ;*/
    if (v_co_geolat != 0 and v_co_geolng != 0 and v_i_geolat != 0 and
       v_i_geolng != 0) then
      parametro := (1 / 180) * 3.14159265358979;
      --REPLACE('222tech', '2', '3'); would return '333tech') --v_geolati:=replace(v_geolati,',','.');
      v_i_geolat  := v_i_geolat * parametro; --p1X
      v_i_geolng  := v_i_geolng * parametro; --p1Y
      v_co_geolat := v_co_geolat * parametro; --p2X
      v_co_geolng := v_co_geolng * parametro; --p2Y

      select acos(sin(v_i_geolng) * sin(v_co_geolng) +
                  Cos(v_i_geolng) * cos(v_co_geolat) *
                  cos(v_i_geolat - v_co_geolat)) * 637.1 /** 1000*/
        into v_distanza
        from dual;

      oDistanza := 'km ' || to_char(round(v_distanza, 2));
    else
      oDistanza := 'non Calcolabile.';
    end if;

    return oDistanza;

  EXCEPTION
    WHEN OTHERS THEN
      -- oMessaggio  :=   'Fallito: ' ||SQLERRM  ;
      return 'non Calcolabile.';
      RAISE;

  end GetDistanzeCOImpianto;

  FUNCTION GetDistanzeCOEdificio(iOidCluster  IN number,
                                 iOidEdificio IN number) Return VARCHAR2 IS

    v_geolati_str          varchar2(100);
    v_geolngi_str          varchar2(100);
    v_geolati              number;
    v_geolngi              number;
    v_geolatr              number;
    v_geolngr              number;
    v_distanza             number;
    parametro              number;
    conta_nr_attivita_sosp number;
    conta_nr_attivita_ass  number;
    v_max                  number;
    ContaIndirizzo         number;
    ContaImpianto          number;
    ContaCluster           number;
    ContaClusterImpianto   number;

    oDistanza   varchar2(100);
    v_co_geolat number;
    v_co_geolng number;
    v_i_geolat  number;
    v_i_geolng  number;
  BEGIN
    --oMessaggio  := '';
    v_co_geolat := 0;
    v_co_geolng := 0;
    v_i_geolat  := 0;
    v_i_geolng  := 0;
    --  calcolo coordinate edificio
    select i.geolat, i.geolng
      into v_i_geolat, v_i_geolng
      from edificio e, indirizzo i
     where e.indirizzo = i.oid
       and e.oid = iOidEdificio;

    -- calcolo coordinate co
    select i.geolat, i.geolng
      into v_co_geolat, v_co_geolng
      from scenario        s,
           clusteredifici  cl,
           centrooperativo co,
           edificio        e,
           indirizzo       i
     where s.oid = cl.scenario
       and s.centrooperativo = co.oid
       and co.edificio = e.oid
       and e.indirizzo = i.oid
       and cl.oid = iOidCluster;
    /*   var milano = new { lat = 45.464194D, lon = 9.188132D };
    var roma = new { lat = 41.904321D, lon = 12.491455D };*/

    -- test -->  v_geolatr:= 41.904321  ; v_geolngr :=9.188132   ;
    -- se esiste il Team allora esiste necessariamente la risorsa capo!
    /*
    v_co_geolat:= 45.464194  ; v_co_geolng :=  9.188132   ;
     v_i_geolat:=  41.904321  ; v_i_geolng := 12.491455   ;*/
    if (v_co_geolat != 0 and v_co_geolng != 0 and v_i_geolat != 0 and
       v_i_geolng != 0) then
      parametro := (1 / 180) * 3.14159265358979;
      --REPLACE('222tech', '2', '3'); would return '333tech') --v_geolati:=replace(v_geolati,',','.');
      v_i_geolat  := v_i_geolat * parametro; --p1X
      v_i_geolng  := v_i_geolng * parametro; --p1Y
      v_co_geolat := v_co_geolat * parametro; --p2X
      v_co_geolng := v_co_geolng * parametro; --p2Y

      select acos(sin(v_i_geolng) * sin(v_co_geolng) +
                  Cos(v_i_geolng) * cos(v_co_geolat) *
                  cos(v_i_geolat - v_co_geolat)) * 637.1 /** 1000*/
        into v_distanza
        from dual;

      oDistanza := 'km ' || to_char(round(v_distanza, 2));
    else
      oDistanza := 'non Calcolabile.';
    end if;

    return oDistanza;

  EXCEPTION
    WHEN OTHERS THEN
      -- oMessaggio  :=   'Fallito: ' ||SQLERRM  ;
      return 'non Calcolabile.';
      RAISE;

  end GetDistanzeCOEdificio;
  FUNCTION GetDescCambioSOperativo(v_stop_pre   IN number,
                                   v_SOperativo IN number) Return VARCHAR2 IS

    MReturn1a VARCHAR2(250);

    MReturn2a VARCHAR2(250);

    MReturn VARCHAR2(250);

  BEGIN
    select so.codstato
      into MReturn1a
      from statooperativo so
     where so.oid = v_stop_pre;

    select so.codstato
      into MReturn2a
      from statooperativo so
     where so.oid = v_SOperativo;

    MReturn := 'da: ' || MReturn1a || ' a: ' || MReturn2a;
    RETURN MReturn;
  EXCEPTION
    WHEN OTHERS THEN
      RETURN 'Fallito: ' || SQLERRM;
      RAISE;
  END GetDescCambioSOperativo;

   procedure RISORSE_EDIFICIO_RDL
    (iOidEdificio in number,
    iusername in varchar2,
    oMessaggio out varchar2,
    IO_CURSOR  IN OUT T_CURSOR)
   is
  V_CURSOR T_CURSOR;
 OidEdificio    Edificio.oid%type;

   begin

  OidEdificio:=iOidEdificio;
  OPEN V_CURSOR FOR
 select co.descrizione as CentroOperativo,
       co.oid as OidCentroOperativo,
       ri.oid as OidRisorsaTeam,
       regrdlultimo.statooperativo as ultimostatooperativo,
       t.nome || ' ' || t.cognome as RisorsaCapoSquadra,
       m.descrizione as Mansione,
       case
         when regrdlag.nrattagenda is null then
          0
         else
          regrdlag.nrattagenda
       end as nrattagenda,
       case
         when regrdlsos.nrattsospese is null then
          0
         else
          regrdlsos.nrattsospese
       end as nrattsospese,
       regrdlemerg.nrattemergenza as nrattemergenza,
       t.telefono,
       regrdlultimo.descrizione as regrdlassociato, --descrizione ultimo registro associato

       (select count(cr.oid)
          from conduttori cr
         where cr.edificio = OidEdificio  -------------------------------########################
         --cr.edificio = distedificio.edificio
           and cr.risorsateam = ri.oid
           ) as Conduttore, --edificio filtrato della regrdl
       ar.nr_risorse as numman, --numrisorse ssociate al team
       u."UserName" as username, --nomerisolto
       distedificio.DISTANZA as distanzaimpianto,
       distedificio.EDIFICIO as edificioregrdl,
       regrdlultimo.cod_edificio as ultimoedificio,
       (select 'nr interventi per edificio ' || count(rdl.oid)
          from rdl
         where rdl.edificio = OidEdificio  -------------------------------########################
         -- rdl.edificio = distedificio.edificio
         ) as interventisuedificio
         ----------------  ag  -------------------
        , distedificio.edificio as oidedificio
-- 'nr interventi per edificio ' as interventisuedificio
--, so.statooperativo as ultimostatooperativo

  from risorseteam ri,
       RISORSE t,
       (select u."Oid", u."UserName" from "SecuritySystemUser" u) u,
       centrooperativo co,
       mansioni m --, statooperativo so
       ,
       (select risorsateam, count(regrdl.rowid) as nrattagenda
          from regrdl
         where regrdl.ultimostatosmistamento in (2)
           and regrdl.ultimostatooperativo = 19
           and regrdl.risorsateam is not null
         group by regrdl.risorsateam) regrdlag,

       (select risorsateam, count(regrdl.rowid) as nrattsospese
          from regrdl
         where regrdl.ultimostatosmistamento in (3)
           and regrdl.ultimostatooperativo in (6, 7, 8, 9, 10)
           and regrdl.risorsateam is not null
         group by regrdl.risorsateam) regrdlsos,

       (select risorsateam, count(regrdl.rowid) as nrattemergenza
          from regrdl
         where regrdl.ultimostatosmistamento in (10)
           and regrdl.risorsateam is not null
         group by regrdl.risorsateam) regrdlemerg,

       (select k.risorsateam,
               regrdl.oid,
               regrdl.descrizione,
               e.oid,
               e.cod_descrizione as cod_edificio,
               so.statooperativo
          from regrdl,
               edificio e,
               impianto i,
               apparato a,
               statooperativo so,
               (select v.risorsateam, v.maxdata, max(regrdl.oid) as maxregrdl
                --so.statooperativo,
                --regrdl.oid, rdl.edificio,rdl.impianto
                  from regrdl,
                       regoperativodettaglio rod --,rdl
                       --,statooperativo so
                      ,
                       (select regrdl.risorsateam, max(t.dataora) as maxdata
                          from regoperativodettaglio t, regrdl
                         where regrdl.oid = t.regrdl
                         group by regrdl.risorsateam) v
                 where v.maxdata = rod.dataora
                   and rod.regrdl = regrdl.oid
                 group by v.risorsateam, v.maxdata) k
         where k.maxregrdl = regrdl.oid
           and regrdl.apparato = a.oid
           and a.impianto = i.oid
          -- and i.edificio = e.oid
           -------------------
           and i.edificio = OidEdificio   ------2342-------------------------########################
           and  e.oid   = OidEdificio   -------2342------------------------########################
           ---------------
           and so.oid = regrdl.ultimostatooperativo) regrdlultimo,
       (select ar.risorseteam, count(ar.rowid) as nr_risorse
          from assrisorseteam ar
         group by ar.risorseteam) ar,
       (select kk.edificio,
               kk.RISORSACAPO,
               kk.centrooperativo,
               CASE
                 WHEN DISTANZA = 0 then
                  'Non Calcolabile '
                 WHEN DISTANZA > 0 and DISTANZA <= 25 THEN
                  'in Prossimità'
                 WHEN DISTANZA > 25 and DISTANZA <= 50 THEN
                  'Vicino'
                 WHEN DISTANZA > 50 THEN
                  'Lontano'
                 ELSE
                  'Non Calcolabile'
               END as DISTANZA

          from (select RISORSACAPO,
                       EDIFICIO,
                       centrooperativo,
                       --INDIRIZZO,
                       acos(round(sin(round(longitudine_edificio, 5)) *
                                  sin(round(longitudine_risorsa, 5)) +
                                  Cos(round(longitudine_edificio, 5)) *
                                  cos(round(longitudine_risorsa, 5)) *
                                  cos(round(latitudine_risorsa, 5) -
                                      round(latitudine_edificio, 5)),
                                  5)) * 6371 DISTANZA
                  from (select nvl(i.geolat, 41.904321) latitudine_edificio,
                               nvl(i.geolng, 12.491455) longitudine_edificio,
                               nvl(r.geolat, 41.904321) latitudine_risorsa,
                               nvl(r.geolng, 12.491455) longitudine_risorsa,
                               --t.Oid RISORSETEAM,
                               e.oid       EDIFICIO,
                               i.oid       INDIRIZZO,
                               c.oid       as centrooperativo,
                               risorsacapo RISORSACAPO
                          from edificio        e,
                               indirizzo       i,
                               risorse         r,
                               risorseteam     t,
                               centrooperativo c,
                               commesse        cm
                         where e.indirizzo = i.oid
                           and e.commessa = cm.oid
                           and r.oid = t.risorsacapo
                           and r.centrooperativo = c.oid
                           and c.areadipolo=cm.areadipolo
                           and e.cod_descrizione not in ('093','PROVA_IARC','PROVA_IARO','LOCALITA-CRV-EDILE','PROVA_IARL','38004-IARO')
                           and t.risorsacapo not in
                           (
                           502,522,523,602,622,623,702,722,723
                           )
                           -----------------
                           and e.oid = OidEdificio   -----------2342--------------------########################
                           ----------------
                        --and r.securityuserid is not null
                        ) vv) kk) distedificio

 where co.oid = t.centrooperativo
      --and co.cod_descrizione='IARCC'
   and ri.risorsacapo = t.oid
   and m.oid = t.mansione
   and ri.oid = regrdlag.risorsateam(+)
   and ri.oid = regrdlsos.risorsateam(+)
   and ri.oid = regrdlemerg.risorsateam(+)
   and ri.oid = regrdlultimo.risorsateam(+)
   and t.securityuserid = u."Oid"(+)
   and ar.risorseteam = ri.oid
   and t.oid || t.centrooperativo =
       distedificio.RISORSACAPO || distedificio.centrooperativo
   and distedificio.EDIFICIO   = OidEdificio  ---------2342----------------------########################

--and so.oid(+)=ri.ultimostatooperativo
;

   IO_CURSOR := V_CURSOR;

  EXCEPTION
    WHEN OTHERS THEN
      oMessaggio := 'Fallito: ' || SQLERRM;
      RAISE;

  end
  RISORSE_EDIFICIO_RDL;

 procedure clona_regrdl_mp (iregrdl in number, omessage out varchar2)
is
  NewRdL        number := 0;
  NewRegRdL     number := 0;
  NewOdL        number := 0;
  NewODldesc    varchar2(250);
  varsysdate    date:=sysdate;
  v_err_code varchar2(250);
  v_err_msg  varchar2(250);
  v_utente   varchar2(250);
  --oldsettimana number:=0;
  --oldmese number:=0;

begin

  /*
      select
      to_number(to_char(regrdl.data_creazione_rdl,'w')),
      to_number(to_char(regrdl.data_creazione_rdl,'mm'))
      into oldsettimana, oldmese
      from REGRDL where oid=iregrdl;
      */
select "sq_REGRDL".Nextval into NewRegRdL from dual;


        insert into REGRDL
        (OID,
        Categoria,
        PRIORITA,
        DESCRIZIONE,
        UTENTE,
        ULTIMADATAAGGIORNAMENTO,
        DATA_CREAZIONE_RDL,
        ULTIMOSTATOSMISTAMENTO,
        REGRDL.Ultimostatooperativo,
        REGRDL.Apparato,
        regrdl.dataassegnazioneodl,
        regrdl.dataupdate,
        regrdl.risorsateam,
        regrdl.data_sopralluogo,
        regrdl.data_azioni_tampone,
        regrdl.datafermo,
        regrdl.datariavvio,
        regrdl.data_inizio_lavori

        )
        select
        NewRegRdL,
        1,
        0,
        'REG. MP('||NewRegRdL||')'
        --||'EDIF. '|| e.cod_descrizione
        --||' IMPIANTO '||i.cod_descrizione ||
        ||' regrdl clonato '||iregrdl as descrizione,
        regrdl.utente,
        varsysdate,
        ADD_MONTHS(regrdl.DATA_CREAZIONE_RDL, 12 ),
        2,
        19,
        regrdl.apparato,
        ADD_MONTHS(regrdl.DATA_CREAZIONE_RDL, 12 ),
        varsysdate,
        regrdl.risorsateam,
        varsysdate+(1/24/60),
        varsysdate+(1/24/60),
        varsysdate+(1/24/60),
        varsysdate+(1/24/60),
        varsysdate+(1/24/60)
        from regrdl, apparato a, impianto i, edificio e
        where
        regrdl.oid=iregrdl
        and a.oid=regrdl.apparato
        and a.impianto=i.oid
        and e.oid=i.edificio ;
        commit;

        select
        v.descrizione into NewODldesc
        from regrdl v
        where v.oid=NewRegRdL;

        update regrdl
        set regrdl.regrdl_successivo=NewRegRdL
        where regrdl.oid=iregrdl;
        commit;

      select regrdl.utente into  v_utente from regrdl
      where regrdl.oid=iregrdl;


  select "sq_ODL".Nextval into NewOdL from dual;
  --select max(oid)+1  into NewOdL from odl;
        insert into odl
        (OID, --1
        DESCRIPTION,--2
        QTYOPENRDL,--3
        REGRDL,--4
        TIPOODL,--5
        STATOOPERATIVO,--6
        DATAEMISSIONE,
        totrisorse
        )

        select
        NewOdL,--1
        NewODldesc,--2
        odl.qtyopenrdl,--3
        NewRegRdL,  --4
        2,--5
        19,--6
        ADD_MONTHS(odl.dataemissione, 12 ),
        odl.totrisorse
        from odl
        where odl.regrdl=iregrdl
        and rownum=1  ;

        commit;

            --scrittura su registri smistamento e operativo---
      insert into regsmistamentodett
        (oid, descrizione, statosmistamento, dataora, regrdl, utente)
      values
        ("sq_REGSMISTAMENTODETT".nextval,
         'Assegnata RegRdL nr' || TO_CHAR(NewRegRdL),
         2,
      (select
      ADD_MONTHS(regrdl.DATA_CREAZIONE_RDL, 12 )
      from regrdl where
      regrdl.oid=iregrdl),
      NewRegRdL,
      v_utente
      );
      commit;

      insert into regoperativodettaglio
        (oid, descrizione, statooperativo, dataora, regrdl, utente)
      values
        ("sq_REGOPERATIVODETTAGLIO".nextval,
         'Assegnata-Da prendere in carico RegRdL nr' || TO_CHAR(NewRegRdL),
         19,
      (select
      ADD_MONTHS(regrdl.DATA_CREAZIONE_RDL, 12 )
      from regrdl where
      regrdl.oid=iregrdl),
      NewRegRdL,
      v_utente
      );
      commit;
      ----------------------------------------------
      ----------------------------------------------

     for nn in (
       select rdl.edificio,
       rdl.impianto,
       rdl.apparato,
       rdl.descrizione || ' regrdl clonato '||iregrdl as descrizione,
       add_months(rdl.datarichiesta,12) as newdata,
       rdl.utenteinserimento,
       rdl.risorsateam,
       c.m_data_azioni_tampone,
       c.m_data_inizio_lavori,
       c.m_data_sopralluogo,
       c.m_datacompletamento,
       c.m_datafermo,
       c.m_datariavvio,
       c.m_pro_cau_rim


       from rdl, impianto i, edificio e, commesse c
       where rdl.regrdl=iregrdl
       and c.oid=e.commessa
       and e.oid=i.edificio
       and rdl.impianto=i.oid
       )
      loop
       select "sq_RDL".Nextval into NewRdL from dual;

      insert into rdl
        (
         oid,
         rdl.regrdl,
         edificio,
         impianto,
         apparato,
         priorita,
         categoria,
         tipointervento,
         descrizione,
         datacreazione,
         datarichiesta,
         utenteinserimento,
         statosmistamento,
         rdl.statooperativo,
         rdl.dataassegnazioneodl,
         rdl.datapianificata,
         rdl.risorsateam,
         rdl.data_sopralluogo,
         rdl.data_azioni_tampone,
         rdl.datafermo,
         rdl.datariavvio,
         rdl.data_inizio_lavori,
         rdl.odl
         )
      values
        (
         NewRdL,
         NewRegRdL,
         nn.edificio,
         nn.impianto,
         nn.apparato,
         0,
         1,
         0,
         nn.descrizione,
         varsysdate,
         nn.newdata,
         nn.utenteinserimento,
         2,
         19,
         nn.newdata,
         nn.newdata,
         nn.risorsateam,
         varsysdate+(1/24/60),
         varsysdate+(1/24/60),
         varsysdate+(1/24/60),
         varsysdate+(1/24/60),
         varsysdate+(1/24/60),
         NewOdL
         );
      commit;


      if nn.m_datafermo > 0 then
      insert into autorizzazioniregrdl
      (oid, regrdl, tipoautorizzazioni)
      values
      ("sq_AUTORIZZAZIONIREGRDL".nextval, NewRegRdL, 0);
      commit;
      end if;
      if nn.m_datariavvio > 0 then
      insert into autorizzazioniregrdl
      (oid, regrdl, tipoautorizzazioni)
      values
      ("sq_AUTORIZZAZIONIREGRDL".nextval, NewRegRdL, 1);
      commit;
      end if;
      if nn.m_data_sopralluogo > 0 then
      insert into autorizzazioniregrdl
      (oid, regrdl, tipoautorizzazioni)
      values
      ("sq_AUTORIZZAZIONIREGRDL".nextval, NewRegRdL, 2);
      commit;
      end if;
      if nn.m_data_azioni_tampone > 0 then
      insert into autorizzazioniregrdl
      (oid, regrdl, tipoautorizzazioni)
      values
      ("sq_AUTORIZZAZIONIREGRDL".nextval, NewRegRdL, 3);
      commit;
      end if;
      if nn.m_data_inizio_lavori > 0 then
      insert into autorizzazioniregrdl
      (oid, regrdl, tipoautorizzazioni)
      values
      ("sq_AUTORIZZAZIONIREGRDL".nextval, NewRegRdL, 4);
      commit;
      end if;
      if nn.m_datacompletamento > 0 then
      insert into autorizzazioniregrdl
      (oid, regrdl, tipoautorizzazioni)
      values
      ("sq_AUTORIZZAZIONIREGRDL".nextval, NewRegRdL, 5);
      commit;
      end if;
      if nn.m_pro_cau_rim > 0 then
      insert into autorizzazioniregrdl
      (oid, regrdl, tipoautorizzazioni)
      values
      ("sq_AUTORIZZAZIONIREGRDL".nextval, NewRegRdL, 6);
      commit;
      end if;






 end loop;
 omessage:='';

EXCEPTION
    WHEN OTHERS THEN
      v_err_code := SQLCODE;
      v_err_msg  := SUBSTR(SQLERRM, 1, 200);
      omessage := v_err_code || '-' || v_err_msg;

end clona_regrdl_mp;

procedure UPDATETEAMCOUNT
  (irisorseteam in number,
  omessage out varchar2 )
  is
  v_err_code varchar2(150):=null;
  v_err_msg  varchar2(150):=null;
  --contoagenda number;
  --contosospese number;
  --regrdlattuale number;
  begin

  insert into tbl_sql
  (s_sql,
   data)
values
  ('PACK MPASSOCARICO - -UPDATETEAMCOUNT'||CHR(13) || CHR(10)||
   'irisorseteam:'||irisorseteam,
   sysdate);
 commit;

  update
  V_RDLRISORSETEAM v
  set
  v.nrattagenda=(select count(regrdl.rowid)
          from regrdl
         where regrdl.ultimostatosmistamento in (2)
           and regrdl.ultimostatooperativo = 19
           and regrdl.risorsateam =irisorseteam
         group by regrdl.risorsateam) ,
  v.nrattsospese=(select count(regrdl.rowid)
          from regrdl
         where regrdl.ultimostatosmistamento in (3)
           and regrdl.ultimostatooperativo in (6, 7, 8, 9, 10)
           and regrdl.risorsateam =irisorseteam
         group by regrdl.risorsateam) ,
  v.nrattemergenza= (select count(regrdl.rowid)
          from regrdl
         where regrdl.ultimostatosmistamento in (10)
           and regrdl.risorsateam =irisorseteam
         group by regrdl.risorsateam) ,
  v.regrdlassociato=(select rt.regrdl  from risorseteam rt
                              where rt.oid=irisorseteam ),

  v.ultimoedificio=
                  (select
                  distinct e.descrizione
                  from
                  regrdl r,
                  rdl,
                  edificio e,
                  (
                  select
                  regrdl.risorsateam,
                  max(regrdl.dataupdate) maxdataupdate
                  from regrdl
                  where regrdl.risorsateam =irisorseteam
                  group by regrdl.risorsateam ) m
                  where r.risorsateam=m.risorsateam
                  and r.dataupdate=m.maxdataupdate
                  and r.oid=rdl.regrdl
                  and rdl.edificio=e.oid)
where
v.oidrisorsateam=irisorseteam;
  commit;


omessage:='';

 EXCEPTION
    WHEN OTHERS THEN
    v_err_code := SQLCODE;
    v_err_msg  := SUBSTR(SQLERRM, 1, 200);
    omessage := v_err_code || '-' || v_err_msg;
end UPDATETEAMCOUNT;

procedure lancioaggrdlbysmistamento(outnum out number)
  is 
  omessaggio varchar(150):='';
  nrgiri number:=0;
begin
  for nr in 
    (select a.oid, a.utente  from AAA a )
   loop     
   AggiornaRdLbySSmistamento(nr.oid, nr.utente,'RdL',omessaggio);
    nrgiri:=nrgiri+1;
   commit;
  end loop;
  outnum:=nrgiri;         
end  lancioaggrdlbysmistamento; 


end PK_MPASSCARICOCAPACITA;
/
