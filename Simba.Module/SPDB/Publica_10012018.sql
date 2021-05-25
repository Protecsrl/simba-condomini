--- aggiornare su DB il campo Descrizione su Registrosmistamentodettaglio
-- Add/modify columns 
alter table REGSMISTAMENTODETT modify descrizione VARCHAR2(250);
 
alter table ODL modify DESCRIPTION VARCHAR2(4000);


-- Aggiornare (update) le date pianificate su RegistroRdL con le date DataAssegnazioneOdl
            rrdl.DataPianificata = rdl.DataPianificata;//  data pianificata  data richieste da bonificare su db 15012018
            rrdl.DataAssegnazioneOdl = rdl.DataAssegnazioneOdl;//  data pianificata  data richieste da bonificare  15012018
update regrdl set DATAPIANIFICATA = DATAASSEGNAZIONEODL
where DATAPIANIFICATA is null
--  verificare se la data DATAASSEGNAZIONEODL è uguale alla data minima (derivata dal mancato funzionamento della sp precedente) aggiornarla in modo cooerente.
-----------  ricercare quindi la data dalla tabella degli audit
select  r.datarichiesta, r.datacompletamento,r.datafermo, r.dataassegnazioneodl, r.rowid
  from rdl r
 where r.dataassegnazioneodl is null
    or r.dataassegnazioneodl < sysdate - 600
 order by 1;

 ---   stato In Attesa di RiAssegnazione (2) è da classificare nella combo smistamento