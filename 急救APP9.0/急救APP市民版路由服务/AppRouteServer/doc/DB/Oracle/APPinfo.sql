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
  is 'APP呼入信息表';
comment on column APPCALLINFO.CASEID
  is '主键，必填，由APP提供，GUID';
comment on column APPCALLINFO.PHONE
  is '主叫号码';
comment on column APPCALLINFO.PROVINCE
  is '省（直辖市）';
comment on column APPCALLINFO.CITY
  is '市';
comment on column APPCALLINFO.AREA
  is '区（县）';
comment on column APPCALLINFO.CALLTIME
  is '必填，呼叫时间';
comment on column APPCALLINFO.ISSELF
  is '必填，1：自己，2：他人';
comment on column APPCALLINFO.NAME
  is '姓名 可以是本人，可以是亲友，由呼叫人指定，路人时候为空';
comment on column APPCALLINFO.SEX
  is '可以是本人，可以是亲友，由呼叫人指定，路人时候为空【0：未知 1：男 2：女】';
comment on column APPCALLINFO.BRITHDAY
  is '出生日期【年月日】';
comment on column APPCALLINFO.HEIGHT
  is '厘米，可以是本人，可以是亲友，由呼叫人指定，路人时候为空';
comment on column APPCALLINFO.WEIGHT
  is '千克，可以是本人，可以是亲友，由呼叫人指定，路人时候为空';
comment on column APPCALLINFO.IDENTITYCARD
  is '身份证号码 可以是本人，可以是亲友，由呼叫人指定，路人时候为空';
comment on column APPCALLINFO.JD
  is '经度';
comment on column APPCALLINFO.WD
  is '纬度';
comment on column APPCALLINFO.ADDRESS
  is '呼叫人地址 报警电话的呼叫地址';
comment on column APPCALLINFO.MEDICALHISTORY
  is '既往病史';
comment on column APPCALLINFO.CONTACTWAY1
  is '联系方式1';
comment on column APPCALLINFO.CONTACTWAY2
  is '联系方式2';
comment on column APPCALLINFO.CONTACTWAY3
  is '联系方式3';
comment on column APPCALLINFO.MEDICALINSURANCECARD
  is '医保卡号';
comment on column APPCALLINFO.ADDTIME
  is '数据添加时间';
comment on column APPCALLINFO.READFLAG
  is '0:未读取    1：已读取   默认：0';
comment on column APPCALLINFO.READTIME
  is '读取时间';
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
  is '车辆轨迹表';
comment on column CARLOCATION.LSH
  is '流水号';
comment on column CARLOCATION.CASEID
  is 'CaseID';
comment on column CARLOCATION.CCCC
  is '出车车次';
comment on column CARLOCATION.CLID
  is '车俩ID';
comment on column CARLOCATION.SJ
  is '坐标数据时间';
comment on column CARLOCATION.JD
  is '经度';
comment on column CARLOCATION.WD
  is '纬度';
comment on column CARLOCATION.SD
  is '速度   单位：KM/H';
comment on column CARLOCATION.FX
  is '方向   角度，垂直向上是0度，顺时针方向，角度增加';
comment on column CARLOCATION.ADDTIME
  is '数据添加时间';

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
  is '车辆状态表';
comment on column CARSTATE.ID
  is '主键 自增1';
comment on column CARSTATE.LSH
  is '流水号';
comment on column CARSTATE.CASEID
  is 'CaseID 由APP提供';
comment on column CARSTATE.CCCC
  is '出车车次';
comment on column CARSTATE.CLID
  is '车俩ID';
comment on column CARSTATE.SJ
  is '状态数据时间';
comment on column CARSTATE.ZT
  is '状态  出车默认不需发送，主要有
1.到达现场，2.病人上车，3.送达医院，4.任务完成。
5.任务中止
';
comment on column CARSTATE.TASK_TERMINATION_REASON
  is '任务中止异常原因描述  1.用户来电取消，2车辆故障，3.病人已康复，4.车道人走，5.拒绝治疗，6.病人已死亡';
comment on column CARSTATE.ADDTIME
  is '数据添加时间';

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
  is 'APP呼救信息错误表';
comment on column HANDLECALLERROR.ID
  is '必填';
comment on column HANDLECALLERROR.CASEID
  is '必填，由APP提供，GUID';
comment on column HANDLECALLERROR.ERRORCODE
  is '错误码';
comment on column HANDLECALLERROR.ERRORMSG
  is '错误内容';
comment on column HANDLECALLERROR.ERRORTIME
  is '错误产生时间';
comment on column HANDLECALLERROR.ADDTIME
  is '数据添加时间';

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
  is '服务质量评价表';
comment on column QUALITYEVALUATION.LSH
  is '服务流水号';
comment on column QUALITYEVALUATION.QUALITYCOMMENT
  is '服务质量评价  0：非常满意
1：满意
2：不满意
经沟通，不使用五星评价制
';
comment on column QUALITYEVALUATION.REASON
  is '原因   不满意时，必填项，满意时可不填写';
comment on column QUALITYEVALUATION.CASEID
  is '由APP提供';
comment on column QUALITYEVALUATION.COMMENTTIME
  is '评价时间';
comment on column QUALITYEVALUATION.ADDTIME
  is '数据添加时间';
comment on column QUALITYEVALUATION.READFLAG
  is '读取标志  0：未读取  1：已读取  默认0';
comment on column QUALITYEVALUATION.READTIME
  is '读取时间';

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
  is '路由信息表';
comment on column ROUTER.ID
  is '主键，自增1';
comment on column ROUTER.XZBM
  is '行政编码';
comment on column ROUTER.PROVINCE
  is '省';
comment on column ROUTER.CITY
  is '市';
comment on column ROUTER.AREA
  is '县（区）';
comment on column ROUTER.ISVALID
  is '是否有效  0：无效  1：有效  默认1';
comment on column ROUTER.ADDTIME
  is '数据添加时间';
comment on column ROUTER.OPERATOR
  is '数据操作人';
comment on column ROUTER.UNITNAME
  is '单位名称';

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
  is '出车信息表';
comment on column SENDCARINFO.ID
  is '主键 自增1';
comment on column SENDCARINFO.CASEID
  is '必填由APP提供';
comment on column SENDCARINFO.LSH
  is '流水号 必填';
comment on column SENDCARINFO.CCCC
  is '出车车次';
comment on column SENDCARINFO.CLID
  is '车俩ID';
comment on column SENDCARINFO.CPH
  is '车牌号';
comment on column SENDCARINFO.CCSJ
  is '出车时间';
comment on column SENDCARINFO.SSDW
  is '车俩所属单位名称';
comment on column SENDCARINFO.DRIVERPHONE
  is '司机电话';
comment on column SENDCARINFO.DRIVERNAME
  is '司机名称';
comment on column SENDCARINFO.DOCTORPHONE
  is '医生电话';
comment on column SENDCARINFO.DOCTORNAME
  is '医生名称';
comment on column SENDCARINFO.ADDTIME
  is '数据添加时间';

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
  is '服务质量信息';
comment on column SERVICEQUALITYINFO.LSH
  is '流水号';
comment on column SERVICEQUALITYINFO.CASEID
  is 'CaseID';
comment on column SERVICEQUALITYINFO.CCCC
  is '出车车次';
comment on column SERVICEQUALITYINFO.CLID
  is '车俩ID';
comment on column SERVICEQUALITYINFO.TIMETAKEN
  is '服务时长  多少分钟，由APP自行计算，此参数取消';
comment on column SERVICEQUALITYINFO.MONEY
  is '服务费用   单位：元';
comment on column SERVICEQUALITYINFO.KM
  is '公里数  单位：公里';
comment on column SERVICEQUALITYINFO.ADDTIME
  is '数据添加时间';

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
