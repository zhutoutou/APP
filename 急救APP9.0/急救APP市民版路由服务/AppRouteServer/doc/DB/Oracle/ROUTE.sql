prompt PL/SQL Developer import file
prompt Created on 2016��6��14�� by jimmy-work
set feedback off
set define off
prompt Loading ROUTER...
insert into ROUTER (ID, XZBM, PROVINCE, CITY, AREA, ISVALID, ADDTIME, OPERATOR, UNITNAME)
values (1, '522121', '����', '�Ͼ�', '�ػ�', 1, to_date('12-06-2016', 'dd-mm-yyyy'), 'admin', '�Ͼ�120��������');
commit;
prompt 1 records loaded
set feedback on
set define on
prompt Done.
