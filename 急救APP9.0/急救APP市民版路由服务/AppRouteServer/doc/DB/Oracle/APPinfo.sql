-----------------------------------------------------
-- Export file for user APPINFO                    --
-- Created by Administrator on 2016-6-27, 16:07:01 --
-----------------------------------------------------

spool APPinfo.log

prompt
prompt Creating table APPCALLINFO
prompt ==========================
prompt
create table APPCALLINFO
(
  CASEID               VARCHAR2(50) not null,
  PHONE                VARCHAR2(20) not null,
  PROVINCE             VARCHAR2(30) not null,
  CITY                 VARCHAR2(30) not null,
  AREA                 VARCHAR2(30),
  CALLTIME             DATE,
  ISSELF               NUMBER(1) default 1 not null,
  NAME                 VARCHAR2(50),
  SEX                  NUMBER(1),
  BRITHDAY             VARCHAR2(8),
  HEIGHT               NUMBER(4),
  WEIGHT               NUMBER(6,2),
  IDENTITYCARD         VARCHAR2(50),
  JD                   VARCHAR2(30),
  WD                   VARCHAR2(30),
  ADDRESS              VARCHAR2(400),
  MEDICALHISTORY       VARCHAR2(500),
  CONTACTWAY1          VARCHAR2(20),
  CONTACTWAY2          VARCHAR2(20),
  CONTACTWAY3          VARCHAR2(20),
  MEDICALINSURANCECARD VARCHAR2(50),
  ADDTIME              DATE,
  READFLAG             NUMBER(1) default 0,
  READTIME             DATE
)
;
comment on table APPCALLINFO
  is 'APP������Ϣ��';
comment on column APPCALLINFO.CASEID
  is '�����������APP�ṩ��GUID';
comment on column APPCALLINFO.PHONE
  is '���к���';
comment on column APPCALLINFO.PROVINCE
  is 'ʡ��ֱϽ�У�';
comment on column APPCALLINFO.CITY
  is '��';
comment on column APPCALLINFO.AREA
  is '�����أ�';
comment on column APPCALLINFO.CALLTIME
  is '�������ʱ��';
comment on column APPCALLINFO.ISSELF
  is '���1���Լ���2������';
comment on column APPCALLINFO.NAME
  is '���� �����Ǳ��ˣ����������ѣ��ɺ�����ָ����·��ʱ��Ϊ��';
comment on column APPCALLINFO.SEX
  is '�����Ǳ��ˣ����������ѣ��ɺ�����ָ����·��ʱ��Ϊ�ա�0��δ֪ 1���� 2��Ů��';
comment on column APPCALLINFO.BRITHDAY
  is '�������ڡ������ա�';
comment on column APPCALLINFO.HEIGHT
  is '���ף������Ǳ��ˣ����������ѣ��ɺ�����ָ����·��ʱ��Ϊ��';
comment on column APPCALLINFO.WEIGHT
  is 'ǧ�ˣ������Ǳ��ˣ����������ѣ��ɺ�����ָ����·��ʱ��Ϊ��';
comment on column APPCALLINFO.IDENTITYCARD
  is '���֤���� �����Ǳ��ˣ����������ѣ��ɺ�����ָ����·��ʱ��Ϊ��';
comment on column APPCALLINFO.JD
  is '����';
comment on column APPCALLINFO.WD
  is 'γ��';
comment on column APPCALLINFO.ADDRESS
  is '�����˵�ַ �����绰�ĺ��е�ַ';
comment on column APPCALLINFO.MEDICALHISTORY
  is '������ʷ';
comment on column APPCALLINFO.CONTACTWAY1
  is '��ϵ��ʽ1';
comment on column APPCALLINFO.CONTACTWAY2
  is '��ϵ��ʽ2';
comment on column APPCALLINFO.CONTACTWAY3
  is '��ϵ��ʽ3';
comment on column APPCALLINFO.MEDICALINSURANCECARD
  is 'ҽ������';
comment on column APPCALLINFO.ADDTIME
  is '�������ʱ��';
comment on column APPCALLINFO.READFLAG
  is '0:δ��ȡ    1���Ѷ�ȡ   Ĭ�ϣ�0';
comment on column APPCALLINFO.READTIME
  is '��ȡʱ��';
alter table APPCALLINFO
  add constraint PK_APPCALLINFO primary key (CASEID);

prompt
prompt Creating table CARLOCATION
prompt ==========================
prompt
create table CARLOCATION
(
  LSH     VARCHAR2(19) not null,
  CASEID  VARCHAR2(50) not null,
  CCCC    VARCHAR2(2) not null,
  CLID    VARCHAR2(10) not null,
  SJ      DATE,
  JD      VARCHAR2(30),
  WD      VARCHAR2(30),
  SD      NUMBER(8,3),
  FX      VARCHAR2(20),
  ADDTIME DATE
)
;
comment on table CARLOCATION
  is '�����켣��';
comment on column CARLOCATION.LSH
  is '��ˮ��';
comment on column CARLOCATION.CASEID
  is 'CaseID';
comment on column CARLOCATION.CCCC
  is '��������';
comment on column CARLOCATION.CLID
  is '����ID';
comment on column CARLOCATION.SJ
  is '��������ʱ��';
comment on column CARLOCATION.JD
  is '����';
comment on column CARLOCATION.WD
  is 'γ��';
comment on column CARLOCATION.SD
  is '�ٶ�   ��λ��KM/H';
comment on column CARLOCATION.FX
  is '����   �Ƕȣ���ֱ������0�ȣ�˳ʱ�뷽�򣬽Ƕ�����';
comment on column CARLOCATION.ADDTIME
  is '�������ʱ��';

prompt
prompt Creating table CARSTATE
prompt =======================
prompt
create table CARSTATE
(
  ID                      NUMBER not null,
  LSH                     VARCHAR2(19) not null,
  CASEID                  VARCHAR2(50) not null,
  CCCC                    NUMBER(2) not null,
  CLID                    VARCHAR2(10) not null,
  SJ                      DATE,
  ZT                      VARCHAR2(20),
  TASK_TERMINATION_REASON VARCHAR2(100),
  ADDTIME                 DATE
)
;
comment on table CARSTATE
  is '����״̬��';
comment on column CARSTATE.ID
  is '���� ����1';
comment on column CARSTATE.LSH
  is '��ˮ��';
comment on column CARSTATE.CASEID
  is 'CaseID ��APP�ṩ';
comment on column CARSTATE.CCCC
  is '��������';
comment on column CARSTATE.CLID
  is '����ID';
comment on column CARSTATE.SJ
  is '״̬����ʱ��';
comment on column CARSTATE.ZT
  is '״̬  ����Ĭ�ϲ��跢�ͣ���Ҫ��
1.�����ֳ���2.�����ϳ���3.�ʹ�ҽԺ��4.������ɡ�
5.������ֹ
';
comment on column CARSTATE.TASK_TERMINATION_REASON
  is '������ֹ�쳣ԭ������  1.�û�����ȡ����2�������ϣ�3.�����ѿ�����4.�������ߣ�5.�ܾ����ƣ�6.����������';
comment on column CARSTATE.ADDTIME
  is '�������ʱ��';

prompt
prompt Creating table HANDLECALLERROR
prompt ==============================
prompt
create table HANDLECALLERROR
(
  ID        NUMBER not null,
  CASEID    VARCHAR2(50) not null,
  ERRORCODE VARCHAR2(10),
  ERRORMSG  VARCHAR2(50),
  ERRORTIME DATE,
  ADDTIME   DATE
)
;
comment on table HANDLECALLERROR
  is 'APP������Ϣ�����';
comment on column HANDLECALLERROR.ID
  is '����';
comment on column HANDLECALLERROR.CASEID
  is '�����APP�ṩ��GUID';
comment on column HANDLECALLERROR.ERRORCODE
  is '������';
comment on column HANDLECALLERROR.ERRORMSG
  is '��������';
comment on column HANDLECALLERROR.ERRORTIME
  is '�������ʱ��';
comment on column HANDLECALLERROR.ADDTIME
  is '�������ʱ��';

prompt
prompt Creating table QUALITYEVALUATION
prompt ================================
prompt
create table QUALITYEVALUATION
(
  LSH            VARCHAR2(19) not null,
  QUALITYCOMMENT VARCHAR2(8) not null,
  REASON         VARCHAR2(400),
  CASEID         VARCHAR2(50) not null,
  COMMENTTIME    DATE,
  ADDTIME        DATE,
  READFLAG       NUMBER(1) default 0,
  READTIME       DATE
)
;
comment on table QUALITYEVALUATION
  is '�����������۱�';
comment on column QUALITYEVALUATION.LSH
  is '������ˮ��';
comment on column QUALITYEVALUATION.QUALITYCOMMENT
  is '������������  0���ǳ�����
1������
2��������
����ͨ����ʹ������������
';
comment on column QUALITYEVALUATION.REASON
  is 'ԭ��   ������ʱ�����������ʱ�ɲ���д';
comment on column QUALITYEVALUATION.CASEID
  is '��APP�ṩ';
comment on column QUALITYEVALUATION.COMMENTTIME
  is '����ʱ��';
comment on column QUALITYEVALUATION.ADDTIME
  is '�������ʱ��';
comment on column QUALITYEVALUATION.READFLAG
  is '��ȡ��־  0��δ��ȡ  1���Ѷ�ȡ  Ĭ��0';
comment on column QUALITYEVALUATION.READTIME
  is '��ȡʱ��';

prompt
prompt Creating table ROUTER
prompt =====================
prompt
create table ROUTER
(
  ID       NUMBER not null,
  XZBM     NVARCHAR2(6) not null,
  PROVINCE NVARCHAR2(30) not null,
  CITY     NVARCHAR2(30) not null,
  AREA     NVARCHAR2(30) not null,
  ISVALID  NUMBER default 1 not null,
  ADDTIME  DATE,
  OPERATOR NVARCHAR2(50),
  UNITNAME NVARCHAR2(50) not null
)
;
comment on table ROUTER
  is '·����Ϣ��';
comment on column ROUTER.ID
  is '����������1';
comment on column ROUTER.XZBM
  is '��������';
comment on column ROUTER.PROVINCE
  is 'ʡ';
comment on column ROUTER.CITY
  is '��';
comment on column ROUTER.AREA
  is '�أ�����';
comment on column ROUTER.ISVALID
  is '�Ƿ���Ч  0����Ч  1����Ч  Ĭ��1';
comment on column ROUTER.ADDTIME
  is '�������ʱ��';
comment on column ROUTER.OPERATOR
  is '���ݲ�����';
comment on column ROUTER.UNITNAME
  is '��λ����';

prompt
prompt Creating table SENDCARINFO
prompt ==========================
prompt
create table SENDCARINFO
(
  ID          NUMBER,
  CASEID      VARCHAR2(50),
  LSH         VARCHAR2(19),
  CCCC        NUMBER(2),
  CLID        VARCHAR2(10),
  CPH         VARCHAR2(20),
  CCSJ        DATE,
  SSDW        VARCHAR2(50),
  DRIVERPHONE VARCHAR2(15),
  DRIVERNAME  VARCHAR2(50),
  DOCTORPHONE VARCHAR2(15),
  DOCTORNAME  VARCHAR2(50),
  ADDTIME     DATE
)
;
comment on table SENDCARINFO
  is '������Ϣ��';
comment on column SENDCARINFO.ID
  is '���� ����1';
comment on column SENDCARINFO.CASEID
  is '������APP�ṩ';
comment on column SENDCARINFO.LSH
  is '��ˮ�� ����';
comment on column SENDCARINFO.CCCC
  is '��������';
comment on column SENDCARINFO.CLID
  is '����ID';
comment on column SENDCARINFO.CPH
  is '���ƺ�';
comment on column SENDCARINFO.CCSJ
  is '����ʱ��';
comment on column SENDCARINFO.SSDW
  is '����������λ����';
comment on column SENDCARINFO.DRIVERPHONE
  is '˾���绰';
comment on column SENDCARINFO.DRIVERNAME
  is '˾������';
comment on column SENDCARINFO.DOCTORPHONE
  is 'ҽ���绰';
comment on column SENDCARINFO.DOCTORNAME
  is 'ҽ������';
comment on column SENDCARINFO.ADDTIME
  is '�������ʱ��';

prompt
prompt Creating table SERVICEQUALITYINFO
prompt =================================
prompt
create table SERVICEQUALITYINFO
(
  LSH       VARCHAR2(19) not null,
  CASEID    VARCHAR2(50) not null,
  CCCC      NUMBER(2) not null,
  CLID      VARCHAR2(10) not null,
  TIMETAKEN NUMBER,
  MONEY     NUMBER,
  KM        NUMBER,
  ADDTIME   DATE
)
;
comment on table SERVICEQUALITYINFO
  is '����������Ϣ';
comment on column SERVICEQUALITYINFO.LSH
  is '��ˮ��';
comment on column SERVICEQUALITYINFO.CASEID
  is 'CaseID';
comment on column SERVICEQUALITYINFO.CCCC
  is '��������';
comment on column SERVICEQUALITYINFO.CLID
  is '����ID';
comment on column SERVICEQUALITYINFO.TIMETAKEN
  is '����ʱ��  ���ٷ��ӣ���APP���м��㣬�˲���ȡ��';
comment on column SERVICEQUALITYINFO.MONEY
  is '�������   ��λ��Ԫ';
comment on column SERVICEQUALITYINFO.KM
  is '������  ��λ������';
comment on column SERVICEQUALITYINFO.ADDTIME
  is '�������ʱ��';

prompt
prompt Creating sequence SQ_CARSTATE_ID
prompt ================================
prompt
create sequence SQ_CARSTATE_ID
minvalue 1
maxvalue 999999999999999999999999999
start with 235
increment by 1
nocache;

prompt
prompt Creating sequence SQ_HANDLECALLERROR_ID
prompt =======================================
prompt
create sequence SQ_HANDLECALLERROR_ID
minvalue 1
maxvalue 999999999999999999999999999
start with 20
increment by 1
nocache;

prompt
prompt Creating sequence SQ_ROUTER_ID
prompt ==============================
prompt
create sequence SQ_ROUTER_ID
minvalue 1
maxvalue 999999999999999999999999999
start with 5
increment by 1
nocache;

prompt
prompt Creating sequence SQ_SENDCARINFO_ID
prompt ===================================
prompt
create sequence SQ_SENDCARINFO_ID
minvalue 1
maxvalue 999999999999999999999999999
start with 94
increment by 1
nocache;

prompt
prompt Creating trigger TR_CARSTATE_ID
prompt ===============================
prompt
CREATE OR REPLACE TRIGGER "TR_CARSTATE_ID"
BEFORE INSERT ON CARSTATE
FOR EACH ROW
begin
select SQ_CARSTATE_ID.nextval into :new.id from dual;
end;
/

prompt
prompt Creating trigger TR_HANDLECALLERROR_ID
prompt ======================================
prompt
CREATE OR REPLACE TRIGGER "TR_HANDLECALLERROR_ID"
BEFORE INSERT ON HANDLECALLERROR
FOR EACH ROW
begin
select SQ_HANDLECALLERROR_ID.nextval into :new.id from dual;
end;
/

prompt
prompt Creating trigger TR_ROUTER_ID
prompt =============================
prompt
CREATE OR REPLACE TRIGGER "TR_ROUTER_ID"
BEFORE INSERT ON ROUTER
FOR EACH ROW
begin
select SQ_ROUTER_ID.nextval into :new.id from dual;
end;
/

prompt
prompt Creating trigger TR_SENDCARINFO_ID
prompt ==================================
prompt
CREATE OR REPLACE TRIGGER "TR_SENDCARINFO_ID"
BEFORE INSERT ON SENDCARINFO
FOR EACH ROW
begin
select SQ_SENDCARINFO_ID.nextval into :new.id from dual;
end;
/


spool off
