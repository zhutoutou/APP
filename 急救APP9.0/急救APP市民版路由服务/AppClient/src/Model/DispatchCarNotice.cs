using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZIT.AppClient.Model
{
    public class receiveAPPSheet //接收APP报警转单
    {
        //caseID
        public string caseid;
        //电话号码
        public string phone;
        //姓名
        public string name;
        //性别
        public string sex;
        //生日
        public string brithday;
        //身高
        public string height;
        //体重
        public string weight;
        //身份证
        public string identitycard;
        //经度
        public string jd;
        //纬度
        public string wd;
        //地址
        public string address;
        //病历
        public string medicalhistory;
        //联系方式1
        public string contactway1;
        //联系方式2
        public string contactway2;
        //联系方式3
        public string contactway3;
        //医保卡号
        public string medicalinsurancecard;
        //地区编码
        public string areacode;
        //省
        public string province;
        //市
        public string city;
        //县
        public string area;
        //呼救时间
        public string calltime;
        //是否本人
        public string isself;
    }

    public class DispatchCarNotice //派车告知
    {
        //流水号
        public string lsh;
        //车辆id
        public string clid;
        //出车车次
        public string cccc;
        //出车时间
        public string ccsj;
        //车牌号
        public string cph;
        //所属单位
        public string ssdw;
        //司机电话
        public string driverphone;
        //司机姓名
        public string drivername;
        //医生电话
        public string doctorphone;
        //医生姓名
        public string doctorname;
        //caseID
        public string caseid;
        //车载电话
        public string tel;
    }

    public class VehicleStatusResponse //车辆状态反馈
    {
        //流水号
        public string lsh;
        //CaseID
        public string caseid;
        //出车车次
        public string cccc;
        //车辆ID
        public string clid;
        //时间
        public string sj;
        //状态
        public string zt;
        //原因
        public string reason;
    }

    public class QualityRespnose  //服务质量反馈
    {
        //流水号
        public string lsh;
        //质量评估
        public string qualitycomment;
        //原因
        public string reason;
        //CaseID
        public string caseid;
        //评论时间
        public string commenttime;
    }

    public class VehiclePointResponse //车载位置反馈
    {
        //CaseID
        public string caseid;
        //LSH
        public string lsh;
        //车次
        public string cc;
        //时间
        public string sj;
        //id
        public string id;
        //经度
        public string jd;
        //纬度
        public string wd;
        //速度
        public string sd;
        //方向
        public string fx;


    }
}
