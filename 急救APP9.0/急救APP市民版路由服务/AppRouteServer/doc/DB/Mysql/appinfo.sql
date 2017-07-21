-- MySQL dump 10.13  Distrib 5.6.24, for Win32 (x86)
--
-- Host: localhost    Database: appinfo
-- ------------------------------------------------------
-- Server version	5.6.24

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `appcallinfo`
--

DROP TABLE IF EXISTS `appcallinfo`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `appcallinfo` (
  `CASEID` varchar(50) NOT NULL COMMENT '主键，必填，由APP提供，GUID',
  `PHONE` varchar(20) NOT NULL COMMENT '主叫号码',
  `PROVINCE` varchar(30) NOT NULL COMMENT '省（直辖市）',
  `CITY` varchar(30) NOT NULL COMMENT '市',
  `AREA` varchar(30) DEFAULT NULL COMMENT '区（县）',
  `CALLTIME` datetime DEFAULT NULL COMMENT '必填，呼叫时间',
  `ISSELF` tinyint(255) NOT NULL DEFAULT '1' COMMENT '必填，1：自己，2：他人',
  `NAME` varchar(50) DEFAULT NULL COMMENT '姓名 可以是本人，可以是亲友，由呼叫人指定，路人时候为空',
  `SEX` tinyint(255) DEFAULT NULL COMMENT '可以是本人，可以是亲友，由呼叫人指定，路人时候为空【0：未知 1：男 2：女】',
  `BRITHDAY` varchar(8) DEFAULT NULL COMMENT '出生日期【年月日】',
  `HEIGHT` int(11) DEFAULT NULL COMMENT '厘米，可以是本人，可以是亲友，由呼叫人指定，路人时候为空',
  `WEIGHT` decimal(6,2) DEFAULT NULL COMMENT '千克，可以是本人，可以是亲友，由呼叫人指定，路人时候为空',
  `IDENTITYCARD` varchar(50) DEFAULT NULL COMMENT '身份证号码 可以是本人，可以是亲友，由呼叫人指定，路人时候为空',
  `JD` varchar(30) DEFAULT NULL COMMENT '经度',
  `WD` varchar(30) DEFAULT NULL COMMENT '纬度',
  `ADDRESS` varchar(400) DEFAULT NULL COMMENT '呼叫人地址 报警电话的呼叫地址',
  `MEDICALHISTORY` varchar(500) DEFAULT NULL COMMENT '既往病史',
  `CONTACTWAY1` varchar(20) DEFAULT NULL COMMENT '联系方式1',
  `CONTACTWAY2` varchar(20) DEFAULT NULL COMMENT '联系方式2',
  `CONTACTWAY3` varchar(20) DEFAULT NULL COMMENT '联系方式3',
  `MEDICALINSURANCECARD` varchar(50) DEFAULT NULL COMMENT '医保卡号',
  `ADDTIME` datetime DEFAULT NULL COMMENT '数据添加时间',
  `READFLAG` tinyint(255) DEFAULT NULL COMMENT '0:未读取    1：已读取   默认：',
  `READTIME` datetime DEFAULT NULL COMMENT '读取时间'
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='APP呼入信息表';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `appcallinfo`
--

LOCK TABLES `appcallinfo` WRITE;
/*!40000 ALTER TABLE `appcallinfo` DISABLE KEYS */;
/*!40000 ALTER TABLE `appcallinfo` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `carlocation`
--

DROP TABLE IF EXISTS `carlocation`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `carlocation` (
  `LSH` varchar(19) NOT NULL COMMENT '流水号',
  `CASEID` varchar(50) NOT NULL COMMENT 'CaseID',
  `CCCC` tinyint(4) NOT NULL COMMENT '出车车次',
  `CLID` varchar(10) NOT NULL COMMENT '车俩ID',
  `SJ` datetime DEFAULT NULL COMMENT '坐标数据时间',
  `JD` varchar(30) DEFAULT NULL COMMENT '经度',
  `WD` varchar(30) DEFAULT NULL COMMENT '纬度',
  `SD` decimal(8,3) DEFAULT NULL COMMENT '速度   单位：KM/H',
  `FX` varchar(20) DEFAULT NULL COMMENT '方向   角度，垂直向上是0度，顺时针方向，角度增加',
  `ADDTIME` datetime DEFAULT NULL COMMENT '数据添加时间'
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='车辆轨迹表';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `carlocation`
--

LOCK TABLES `carlocation` WRITE;
/*!40000 ALTER TABLE `carlocation` DISABLE KEYS */;
/*!40000 ALTER TABLE `carlocation` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `carstate`
--

DROP TABLE IF EXISTS `carstate`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `carstate` (
  `ID` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '主键 自增1',
  `LSH` varchar(19) NOT NULL COMMENT '流水号',
  `CASEID` varchar(50) NOT NULL COMMENT 'CaseID 由APP提供',
  `CCCC` tinyint(6) NOT NULL COMMENT '出车车次',
  `CLID` varchar(10) NOT NULL COMMENT '车俩ID',
  `SJ` datetime DEFAULT NULL COMMENT '状态数据时间',
  `ZT` varchar(20) DEFAULT NULL COMMENT '状态  出车默认不需发送，主要有 1.到达现场，2.病人上车，3.送达医院，4.任务完成。 5.任务中止',
  `TASK_TERMINATION_REASON` varchar(100) DEFAULT NULL COMMENT '任务中止异常原因描述  1.用户来电取消，2车辆故障，3.病人已康复，4.车道人走，5.拒绝治疗，6.病人已死亡',
  `ADDTIME` datetime DEFAULT NULL COMMENT '数据添加时间',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='车辆状态表';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `carstate`
--

LOCK TABLES `carstate` WRITE;
/*!40000 ALTER TABLE `carstate` DISABLE KEYS */;
/*!40000 ALTER TABLE `carstate` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `handlecallerror`
--

DROP TABLE IF EXISTS `handlecallerror`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `handlecallerror` (
  `ID` bigint(20) NOT NULL AUTO_INCREMENT,
  `CASEID` varchar(50) NOT NULL COMMENT '必填，由APP提供，GUID',
  `ERRORCODE` varchar(10) DEFAULT NULL COMMENT '错误码',
  `ERRORMSG` varchar(50) DEFAULT NULL COMMENT '错误内容',
  `ERRORTIME` datetime DEFAULT NULL COMMENT '错误产生时间',
  `ADDTIME` datetime DEFAULT NULL COMMENT '数据添加时间',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='APP呼救信息错误表';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `handlecallerror`
--

LOCK TABLES `handlecallerror` WRITE;
/*!40000 ALTER TABLE `handlecallerror` DISABLE KEYS */;
/*!40000 ALTER TABLE `handlecallerror` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `qualityevaluation`
--

DROP TABLE IF EXISTS `qualityevaluation`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `qualityevaluation` (
  `LSH` varchar(19) NOT NULL COMMENT '服务流水号',
  `QUALITYCOMMENT` varchar(8) NOT NULL COMMENT '服务质量评价  0：非常满意 1：满意 2：不满意',
  `REASON` varchar(400) DEFAULT NULL COMMENT '原因   不满意时，必填项，满意时可不填写',
  `CASEID` varchar(50) NOT NULL COMMENT '由APP提供',
  `COMMENTTIME` datetime DEFAULT NULL COMMENT '评价时间',
  `ADDTIME` datetime DEFAULT NULL COMMENT '数据添加时间',
  `READFLAG` tinyint(255) DEFAULT NULL COMMENT '读取标志  0：未读取  1：已读取  默认0',
  `READTIME` datetime DEFAULT NULL COMMENT '读取时间'
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='服务质量评价表';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `qualityevaluation`
--

LOCK TABLES `qualityevaluation` WRITE;
/*!40000 ALTER TABLE `qualityevaluation` DISABLE KEYS */;
/*!40000 ALTER TABLE `qualityevaluation` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `router`
--

DROP TABLE IF EXISTS `router`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `router` (
  `ID` bigint(255) NOT NULL AUTO_INCREMENT COMMENT '主键，自增1',
  `XZBM` varchar(6) NOT NULL COMMENT '行政编码',
  `PROVINCE` varchar(30) NOT NULL COMMENT '省',
  `CITY` varchar(30) NOT NULL COMMENT '市',
  `AREA` varchar(30) DEFAULT NULL COMMENT '县（区）',
  `ISVALID` tinyint(4) NOT NULL DEFAULT '1' COMMENT '是否有效  0：无效  1：有效  默认1',
  `ADDTIME` datetime DEFAULT NULL COMMENT '数据添加时间',
  `OPERATOR` varchar(50) DEFAULT NULL COMMENT '数据操作人',
  `UNITNAME` varchar(100) NOT NULL COMMENT '单位名称',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='路由信息表';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `router`
--

LOCK TABLES `router` WRITE;
/*!40000 ALTER TABLE `router` DISABLE KEYS */;
/*!40000 ALTER TABLE `router` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sendcarinfo`
--

DROP TABLE IF EXISTS `sendcarinfo`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `sendcarinfo` (
  `ID` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '主键 自增1',
  `CASEID` varchar(50) DEFAULT NULL COMMENT '必填由APP提供',
  `LSH` varchar(19) DEFAULT NULL COMMENT '流水号 必填',
  `CCCC` tinyint(4) DEFAULT NULL COMMENT '出车车次',
  `CLID` varchar(10) DEFAULT NULL COMMENT '车俩ID',
  `CPH` varchar(20) DEFAULT NULL COMMENT '车牌号',
  `CCSJ` datetime DEFAULT NULL COMMENT '出车时间',
  `SSDW` varchar(50) DEFAULT NULL COMMENT '车俩所属单位名称',
  `DRIVERPHONE` varchar(15) DEFAULT NULL COMMENT '司机电话',
  `DRIVERNAME` varchar(50) DEFAULT NULL COMMENT '司机名称',
  `DOCTORPHONE` varchar(15) DEFAULT NULL COMMENT '医生电话',
  `DOCTORNAME` varchar(50) DEFAULT NULL COMMENT '医生名称',
  `ADDTIME` datetime DEFAULT NULL COMMENT '数据添加时间',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='出车信息表';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sendcarinfo`
--

LOCK TABLES `sendcarinfo` WRITE;
/*!40000 ALTER TABLE `sendcarinfo` DISABLE KEYS */;
/*!40000 ALTER TABLE `sendcarinfo` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `servicequalityinfo`
--

DROP TABLE IF EXISTS `servicequalityinfo`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `servicequalityinfo` (
  `LSH` varchar(19) NOT NULL COMMENT '流水号',
  `CASEID` varchar(50) NOT NULL COMMENT 'CaseID',
  `CCCC` tinyint(4) NOT NULL COMMENT '出车车次',
  `CLID` varchar(10) NOT NULL COMMENT '车俩ID',
  `TIMETAKEN` int(11) DEFAULT NULL COMMENT '服务时长  多少分钟，由APP自行计算，此参数取消',
  `MONEY` int(11) DEFAULT NULL COMMENT '服务费用   单位：元',
  `KM` int(11) DEFAULT NULL COMMENT '公里数  单位：公里',
  `ADDTIME` datetime DEFAULT NULL COMMENT '数据添加时间'
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='服务质量信息';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `servicequalityinfo`
--

LOCK TABLES `servicequalityinfo` WRITE;
/*!40000 ALTER TABLE `servicequalityinfo` DISABLE KEYS */;
/*!40000 ALTER TABLE `servicequalityinfo` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2016-06-27 17:38:12
