CREATE OR REPLACE TRIGGER "RDL_TT_MP"
  BEFORE INSERT ON RDL
  FOR EACH ROW
DECLARE
  oidcategoria   number;
  numeromax      number;
  valoreiniziale number;
  --  WHEN ((NEW.rrdlttmp IS NULL or NEW.rrdlttmp = 0) and NEW.categoria = 5)
BEGIN
  valoreiniziale := Nvl(:NEW.rrdlttmp, 0);
  
  if valoreiniziale = 0 then
    SELECT :new.categoria INTO oidcategoria FROM DUAL;  
    if (oidcategoria = 5 or oidcategoria = 1) then
      -- man programmata
      SELECT max(t.rrdlttmp)
        INTO numeromax
        FROM rdl t
       where t.categoria in (1, 5);
      :NEW.rrdlttmp := numeromax + 1;
      --   SELECT SEQ_rrdltt.NEXTVAL INTO :NEW.rrdlttmp FROM DUAL;
    else
      SELECT max(t.rrdlttmp)
        INTO numeromax
        FROM rdl t
       where t.categoria not in (1, 5);
      :NEW.rrdlttmp := numeromax + 1;
      -- SELECT SEQ_rrdltt.NEXTVAL INTO :NEW.rrdlttmp FROM DUAL;
    end if;
  end if;

END;

  /* CREATE OR REPLACE TRIGGER trg_before_emp_insr_userinfo
  BEFORE INSERT
    ON employee_details
    FOR EACH ROW
  
  
  
  BEGIN
  
    -- Replaced by the current logged in user "HR" by a trigger.
    SELECT USER INTO username FROM dual;
  
    -- Setting created_by and created_Date values.
    :NEW.CREATED_BY := username;
    :NEW.CREATED_DATE := sysdate;
  
  END;*/

  /*DECLARE
  oidcategoria number;*/

  /*--WHEN ((NEW.rrdlttmp IS NULL or NEW.rrdlttmp = 0) ) --and NEW.categoria != 5)
  BEGIN
     SELECT NEW.categoria.Oid INTO oidcategoria FROM DUAL;
  IF oidcategoria != 5 THEN
           SELECT SEQ_rrdltt.NEXTVAL INTO :NEW.rrdlttmp FROM DUAL;
          ELSE 
              SELECT SEQ_rrdltt.NEXTVAL INTO :NEW.rrdlttmp FROM DUAL;
      END IF;
  
  \*   when case (NEW.categoria != 5)
    SELECT SEQ_rrdltt.NEXTVAL INTO :NEW.rrdlttmp FROM DUAL;
    else
       SELECT SEQ_rrdltt.NEXTVAL INTO :NEW.rrdlttmp FROM DUAL;
       end;
      *\
  END;*/
/
