
 pubblica v_rdl_list_guasto vista

select t.*,t.rowid from COMMESSETINTERVENTO t
update  COMMESSETINTERVENTO t set t.defaultvalue = 1
where t.tipointervento = 110


select t.*,t.rowid from COMMESSEPRIORITA t
update  COMMESSEPRIORITA t set t.defaultvalue = 1
where t.priorita = 2
