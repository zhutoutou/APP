prompt PL/SQL Developer import file
prompt Created on 2016年6月14日 by jimmy-work
set feedback off
set define off
prompt Loading ROUTER...
insert into ROUTER (ID, XZBM, PROVINCE, CITY, AREA, ISVALID, ADDTIME, OPERATOR, UNITNAME)
values (1, '522121', '江苏', '南京', '秦淮', 1, to_date('12-06-2016', 'dd-mm-yyyy'), 'admin', '南京120急救中心');
commit;
prompt 1 records loaded
set feedback on
set define on
prompt Done.
