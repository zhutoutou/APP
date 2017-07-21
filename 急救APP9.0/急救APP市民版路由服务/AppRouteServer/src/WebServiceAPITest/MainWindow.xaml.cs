using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WebServiceAPITest
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void btnCallIn_Click(object sender, RoutedEventArgs e)
        {
            txtContent.Text = "";
            
            AppRoute.AppRouteServiceSoapClient client = new AppRoute.AppRouteServiceSoapClient();

            

            AppRoute.CredentialSoapHeader header = new AppRoute.CredentialSoapHeader();
            header.Username = "120_user";
            header.Password = "120_password";

            /*" {  \"phone\": \"\", \"name\": null, 
                "sex": null, 
                "brithday": "19820101",
                "height": 170, 
                "weight": 130,
                "identityCard": null, 
                "jd": null, 
                "wd": null, 
                "address": "江苏省南京市秦淮区太平南路333号", 
                "medicalHistory": "既往病史", 
                "contactWay1":"13404178872" , 
                "contactWay2": null, 
                "contactWay3": null,     
                "medicalInsuranceCard": null, 
                "caseId": null, 
                "province": "江苏省",
                "city": "南京市",
                "area": "江宁区",
                "areaCode": "行政区号",
                "callTime": null, 
                "isSelf": null
                }"
           */

            Hashtable htCallInfo = new Hashtable();
            htCallInfo.Add("phone", "12345");
            htCallInfo.Add("name", "测试名");
            htCallInfo.Add("sex", "1");
            htCallInfo.Add("brithday", "19870101");
            htCallInfo.Add("height", "175");
            htCallInfo.Add("weight", "65");
            htCallInfo.Add("identityCard", "340879198808081211");
            htCallInfo.Add("jd", "34.799686555");
            htCallInfo.Add("wd", "134.799686555");
            htCallInfo.Add("address", "江苏省南京市秦淮区太平南路333号");
            htCallInfo.Add("medicalHistory", "测试病史");
            htCallInfo.Add("contactWay1", "13404178872");
            htCallInfo.Add("contactWay2", "13404178873");
            htCallInfo.Add("contactWay3", "13404178874");
            htCallInfo.Add("medicalInsuranceCard", "232335567787");
            htCallInfo.Add("caseId", Guid.NewGuid().ToString());
            htCallInfo.Add("province", "江苏");
            htCallInfo.Add("city", "南京");
            htCallInfo.Add("area", "秦淮");
            //htCallInfo.Add("areaCode", "");
            htCallInfo.Add("callTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            htCallInfo.Add("isSelf", "1");

            string strCallInfoJson = ZIT.AppRouteServer.Utility.JSON.Encode(htCallInfo);
            txtContent.Text =  client.addCallInfo(header, strCallInfoJson);

        }

        private void btnQualityComment_Click(object sender, RoutedEventArgs e)
        {
            txtContent.Text = "";
            AppRoute.AppRouteServiceSoapClient client = new AppRoute.AppRouteServiceSoapClient();

            AppRoute.CredentialSoapHeader header = new AppRoute.CredentialSoapHeader();
            header.Username = "120_user";
            header.Password = "120_password";

            /*
             * lsh;	String	服务流水号	Varchar2(50)，not null	50	主键，120自动生成，并发送给APP的
                qualityComment	String	服务质量评价	Varchar2(8)	8	0：非常满意
                1：满意
                2：不满意
                经沟通，不使用五星评价制
                reason	String	原因	Varchar2 (400)	400	不满意时，必填项，满意时可不填写
                caseId	String	CaseID	Varchar2(20)	20	由APP提供
                commentTime	Date  	评价时间	Date		
             */

            Hashtable htQualityComment = new Hashtable();
            htQualityComment.Add("lsh", "2016061200000019999");
            htQualityComment.Add("qualityComment", "0");
            htQualityComment.Add("reason", "");
            htQualityComment.Add("caseId", "80fad1b3-08c1-4032-9e2d-0544c49b4c9a");
            htQualityComment.Add("commentTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            string strQualityCommenJson = ZIT.AppRouteServer.Utility.JSON.Encode(htQualityComment);
            txtContent.Text = client.addQualityComment(header, strQualityCommenJson);

        }
    }
}
