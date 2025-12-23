
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace ProjectHoneyPot.Controllers
{
    //   PROJECT HONEY POT ADDRESS DISTRIBUTION SCRIPT
    //   For more information visit: http://www.projecthoneypot.org/
    //   Copyright (C) 2004-2025, Unspam Technologies, Inc.
    //   
    //   This program is free software; you can redistribute it and/or modify
    //   it under the terms of the GNU General Public License as published by
    //   the Free Software Foundation; either version 2 of the License, or
    //   (at your option) any later version.
    //   
    //   This program is distributed in the hope that it will be useful,
    //   but WITHOUT ANY WARRANTY; without even the implied warranty of
    //   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    //   GNU General Public License for more details.
    //   
    //   You should have received a copy of the GNU General Public License
    //   along with this program; if not, write to the Free Software
    //   Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA
    //   02111-1307  USA
    //   
    //   If you choose to modify or redistribute the software, you must
    //   completely disconnect it from the Project Honey Pot Service, as
    //   specified under the Terms of Service Use. These terms are available
    //   here:
    //   
    //   http://www.projecthoneypot.org/terms_of_service_use.php
    //   
    //   The required modification to disconnect the software from the
    //   Project Honey Pot Service is explained in the comments below. To find the
    //   instructions, search for:  *** DISCONNECT INSTRUCTIONS ***
    //   
    //   Generated On: Fri, 12 Dec 2025 10:51:08 -0500
    //   For Domain: kpfanworld.com
    //   
    //   

    //   *** DISCONNECT INSTRUCTIONS ***
    //   
    //   You are free to modify or redistribute this software. However, if
    //   you do so you must disconnect it from the Project Honey Pot Service.
    //   To do this, you must delete the lines of code below located between the
    //   *** START CUT HERE *** and *** FINISH CUT HERE *** comments. Under the
    //   Terms of Service Use that you agreed to before downloading this software,
    //   you may not recreate the deleted lines or modify this software to access
    //   or otherwise connect to any Project Honey Pot server.

    public partial class HoneypotController(IHttpContextAccessor httpContextAccessor, IConfiguration configuration) : Controller
    {
        //   *** START CUT HERE ***
        //   
        const string REQUEST_HOST = "hpr8.projecthoneypot.org";
        const string REQUEST_SCRIPT = "/cgi/serve.php";
        //   
        //   *** FINISH CUT HERE ***
        //   

        const string HPOT_TAG1 = "d2dab03dbb30a8698c3e24fe2831b002";
        const string HPOT_TAG2 = "ef5be4609a4d4fe8c38b50986abcb0ad";
        const string HPOT_TAG3 = "835ea5f09d8d7f83ff5c99112b6b5af9";

        const string CLASS_STYLE_1 = "frucutru";
        const string CLASS_STYLE_2 = "stad";

        const string DIV1 = "n6sl7uih";

        const string VANITY_L1 = "MEMBER OF PROJECT HONEY POT";
        const string VANITY_L2 = "Spam Harvester Protection Network";
        const string VANITY_L3 = "provided by Unspam";

        const string DOC_TYPE1 = "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.01 Transitional//EN\" \"http://www.w3.org/TR/html4/loose.dtd\">\n";
        const string HEAD1 = "<html>\n<head>\n";
        const string HEAD2 = "<title>Disposaladdress</title>\n</head>\n";
        const string ROBOT1 = "<meta name=\"robots\" content=\"follow,noindex,noarchive\">\n";
        const string NOCOLLECT1 = "<meta name=\"no-email-collection\" content=\"/\">\n";
        const string TOP1 = "<body>\n<center>\n";
        const string EMAIL1A = "<a href=\"mailto:";
        const string EMAIL1B = "\" style=\"display: none;\">";
        const string EMAIL1C = "</a>";
        const string EMAIL2A = "<a href=\"mailto:";
        const string EMAIL2B = "\" style=\"display:none;\">";
        const string EMAIL2C = "</a>";
        const string EMAIL3A = "<a style=\"display: none;\" href=\"mailto:";
        const string EMAIL3B = "\">";
        const string EMAIL3C = "</a>";
        const string EMAIL4A = "<a style=\"display:none;\" href=\"mailto:";
        const string EMAIL4B = "\">";
        const string EMAIL4C = "</a>";
        const string EMAIL5A = "<a href=\"mailto:";
        const string EMAIL5B = "\"></a>";
        const string EMAIL5C = "..";
        const string EMAIL6A = "<span style=\"display: none;\"><a href=\"mailto:";
        const string EMAIL6B = "\">";
        const string EMAIL6C = "</a></span>";
        const string EMAIL7A = "<span style=\"display:none;\"><a href=\"mailto:";
        const string EMAIL7B = "\">";
        const string EMAIL7C = "</a></span>";
        const string EMAIL8A = "<!-- <a href=\"mailto:";
        const string EMAIL8B = "\">";
        const string EMAIL8C = "</a> -->";
        const string EMAIL9A = "<div id=\"'.__DIV1.'\"><a href=\"mailto:";
        const string EMAIL9B = "\">";
        const string EMAIL9C = "</a></div><br><script language=\"JavaScript\" type=\"text/javascript\">document.getElementById(\''.__DIV1.'\').innerHTML=\'\';</script>";
        const string EMAIL10A = "<a href=\"mailto:";
        const string EMAIL10B = "\"><!-- ";
        const string EMAIL10C = " --></a>";
        const string LEGAL1 = "";
        const string LEGAL2 = "\n";
        const string STYLE1 = "\n<style>a.'.__CLASS_STYLE_1.'{color:#FFF;font:bold 10px arial,sans-serif;text-decoration:none;}</style>";
        const string VANITY1 = "<table cellspacing=\"0\"cellpadding=\"0\"border=\"0\"style=\"background:#999;width:230px;\"><tr><td valign=\"top\"style=\"padding: 1px 2px 5px 4px;border-right:solid 1px #CCC;\"><span style=\"font:bold 30px arial,sans-serif;color:#666;top:0px;position:relative;\">@</span></td><td valign=\"top\" align=\"left\" style=\"padding:3px 0 0 4px;\"><a href=\"http://www.projecthoneypot.org/\" class=\"'.__CLASS_STYLE_1.'\">'.__VANITY_L1.'</a><br><a href=\"http://www.unspam.com\"class=\"'.__CLASS_STYLE_1.'\">'.__VANITY_L2.'<br>'.__VANITY_L3.'</a></td></tr></table>\n";
        const string BOTTOM1 = "</center>\n</body>\n</html>\n";

        static string GetLegalContent() { return "<table cellpadding=\"0\" border=\"0\" cellspacing=\"0\"><tr>\n<td><font face=\"courier\">&nbsp; &nbsp;&nbsp; <br>&nbsp;<br>The<span style=\"color:#FFF;\">d</span>w<br>to yo<br>other<br>W&#101;bsi<br>read<span style=\"color:#FFF;\">c</span><br>a&#103;ent<br>them.<br>non-&#116;<br>W&#101;bsi<br><br>&nbsp; &nbsp;&nbsp; <br>&nbsp;<br>Spec&#105;<br>Non-H<br>spide<br>p&#114;ogr<br>au&#116;om<br><br>Email<br>It is<br>alo&#110;e<br>has a<br>stora<br>value<br>&#115;tori<br>agree<br><br>&nbsp;<b><span style=\"color:#FFF;\">d</span></b>&nbsp;&nbsp; <br>&nbsp;<br>Eac&#104; <br>again<br>&#40;\"&#74;ud<br>the r<br>such <br>a&#110;d<span style=\"color:#FFF;\">o</span>p<br>of fe<br>any &#97;<br>Ser&#118;i<br>the a<br><br>&nbsp;<b><span style=\"color:#FFF;\">s</span></b>&nbsp;&nbsp; <br>&nbsp;<br>Yo&#117; c<br>may<span style=\"color:#FFF;\">f</span>&#97;<br>abuse<br>Visit<br><br>VI&#83;IT<br>P&#65;&#82;&#84;Y<br>SUBSE<br></font></td>\n<td><font face=\"courier\">&nbsp; &nbsp; <b><span style=\"color:#FFF;\">g</span></b><br><br>e&#98;s&#105;t<br>u sub<br>&nbsp;term<br>te yo<br>them <br>s of <br>&nbsp;&#84;he <br>&#114;ansf<br>te.<br><br><b><span style=\"color:#FFF;\">o</span></b>&nbsp; &nbsp; <br><br>al re<br>uman <br>rs&#44; b<br>am&#115; d<br>&#97;tica<br><br>&nbsp;a&#100;dr<br>&nbsp;r&#101;co<br>. You<br>&nbsp;valu<br>g&#101;, a<br>&nbsp;of &#116;<br>ng th<br>ment <br><br>&nbsp; &nbsp;&nbsp; <br><br>&#112;arty<br>st &#116;h<br>icial<br>egi&#115;&#116;<br>laws <br>erfor<br>d&#101;r&#97;l<br>ct&#105;o&#110;<br>ce. Y<br>bove<span style=\"color:#FFF;\">e</span><br><br>&nbsp; &nbsp;&nbsp; <br><br>onse&#110;<br>&#112;pear<br>. &#84;he<br>ors a<br><br>ORS A<br>&nbsp;O&#82; S<br>QU&#69;NT<br></font></td>\n<td><font face=\"courier\">&nbsp; &nbsp;&nbsp; <br><br>e<span style=\"color:#FFF;\">p</span>&#102;ro<br>jec&#116;<span style=\"color:#FFF;\">d</span><br>s &#103;ov<br>u acc<br>&#99;&#97;r&#101;&#102;<br>the i<br>acces<br>&#101;rabl<br><br><br><b><span style=\"color:#FFF;\">p</span></b>&nbsp;<b><span style=\"color:#FFF;\">c</span></b>&nbsp; <br><br>stric<br>V&#105;sit<br>ots,<span style=\"color:#FFF;\">c</span><br>esign<br>&#108;ly.<br><br>ess&#101;s<br>gnize<br>&nbsp;ackn<br>e not<br>nd/or<br>h&#101;se<span style=\"color:#FFF;\">f</span><br>is We<br>&#97;nd e<br><br>&nbsp; &nbsp;&nbsp; <br><br>&nbsp;ag&#114;&#101;<br>e ot&#104;<br><span style=\"color:#FFF;\">s</span>Acti<br>ered <br>are &#97;<br>med<span style=\"color:#FFF;\">t</span>e<br>&nbsp;a&#110;d <br>&nbsp;brou<br>ou co<br>a&#103;ree<br><br>&nbsp; &nbsp;&nbsp; <br><br>t to <br>&nbsp;so&#109;e<br>&nbsp;Iden<br>gree <br><br>GREE<span style=\"color:#FFF;\">s</span><br>ENDIN<br>&nbsp;BREA<br></font></td>\n<td><font face=\"courier\">&nbsp; &nbsp;&nbsp; <br><br>m whi<br>to th<br>&#101;rnin<br>ept<span style=\"color:#FFF;\">a</span>&#116;<br>&#117;l&#108;y.<br>ndivi<br>s ri&#103;<br>&#101; wi&#116;<br><br><br><b>SPECI</b><br><br>tion&#115;<br>ors. <br>index<br>&#101;d &#116;o<br><br><br>&nbsp;on<span style=\"color:#FFF;\">c</span>t<br>d<span style=\"color:#FFF;\">t</span>tha<br>owle&#100;<br>&nbsp;&#108;e&#115;s<br>&nbsp;dist<br>addre<br>bsite<br>xpres<br><br>&nbsp;<b><span style=\"color:#FFF;\">t</span></b>&nbsp;&nbsp; <br><br>es &#116;h<br>er in<br>on\") <br>Adm&#105;n<br>&#112;plie<br>ntire<br>state<br>ght a<br>nsent<br>ment&#46;<br><br>&nbsp; <b><span style=\"color:#FFF;\">g</span><span style=\"color:#FFF;\">f</span><span style=\"color:#FFF;\">d</span></b><br><br>havin<br>&#119;here<br>tifi&#101;<br>&#110;ot t<br><br>THAT <br>G ANY<br>CH &#79;&#70;<br></font></td>\n<td><font face=\"courier\"><b><span style=\"color:#FFF;\">k</span></b>&nbsp; <b><span style=\"color:#FFF;\">p</span></b>&nbsp;<br><br>ch yo<br>e fol<br>g acc<br>h&#101;&#115;e<span style=\"color:#FFF;\">k</span><br>&nbsp;&#65;n&#121; <br>du&#97;l(<br>hts g<br>hout <br><br><br><b>AL<span style=\"color:#FFF;\">a</span>LI</b><br><br>&nbsp;on a<br>N&#111;n-H<br>ers, <br>&nbsp;acce<br><br><br>his s<br>&#116; th&#101;<br>ge<span style=\"color:#FFF;\">h</span>a&#110;<br>&nbsp;&#116;han<br>ribut<br>sse&#115;.<br>'s &#101;m<br>sly p<br><br>&nbsp; &nbsp;&nbsp; <br><br>at an<br>&nbsp;conn<br>s&#104;all<br>is&#116;ra<br>d &#116;o <br>ly wi<br>&nbsp;co&#117;r<br>g&#97;ins<br>&nbsp;to e<br><br><br>&nbsp; &nbsp; <b>R</b><br><br>g you<br>&nbsp;on t<br>r is <br>o use<br><br>HARVE<br>&nbsp;&#77;ES&#83;<br>&nbsp;THES<br></font></td>\n<td><font face=\"courier\">&nbsp; <b>TE&#82;</b><br><br>&#117; acc<br>lowin<br>es&#115; t<br>terms<br>Non-H<br>s) wh<br>&#114;a&#110;te<br>th&#101; e<br><br><br><b>CENSE</b><br><br>&nbsp;&#118;isi<br>uman <br>robot<br>ss, r<br><br><br>ite a<br>&#115;&#101; em<br>d &#97;gr<br>&nbsp;US $<br>io&#110;<span style=\"color:#FFF;\">p</span>o<br>&nbsp;In&#116;e<br>ail a<br>&#114;ohi&#98;<br><br><b>APPLI</b><br><br>y sui<br>ectio<br><span style=\"color:#FFF;\">f</span>be g<br>tive <br>agree<br>thin <br>ts &#119;i<br>t &#104;im<br>l&#101;&#99;tr<br><br><br><b>ECORD</b><br><br>r Int<br>his p<br>uniq&#117;<br>&nbsp;th&#105;s<br><br>STING<br>AG&#69;&#40;S<br>E TER<br></font></td>\n<td><font face=\"courier\"><b>MS</b>&nbsp;<b>A&#78;</b><br><br>essed<br>g con<br>o t&#104;e<br>&nbsp;and <br>&#117;ma&#110; <br>o co&#110;<br>d to <br>xp&#114;es<br><br><br>&nbsp;<b>REST</b><br><br>t&#111;r's<br>Visit<br>&#115;, cr<br>&#101;ad, <br><br><br>&#114;e co<br>a&#105;l a<br>ee th<br>&#53;&#48;. Y<br>f the<br>ntion<br>ddres<br>ited.<br><br><b>CAB&#76;E</b><br><br>t, ac<br>n w&#105;t<br>overn<br>Conta<br>ments<br>the &#65;<br>thin <br><span style=\"color:#FFF;\">o</span>&#105;n c<br>onic <br><br><br><b>&#83;<span style=\"color:#FFF;\">o</span>O&#70;</b>&nbsp;<br><br>er&#110;et<br>age (<br>ely m<br><span style=\"color:#FFF;\">g</span>a&#100;dr<br><br>, G&#65;T<br>) TO <br>MS O&#70;<br></font></td>\n<td><font face=\"courier\"><b>D</b>&nbsp;<b>C&#79;&#78;</b><br><br>&nbsp;th&#105;s<br>ditio<br>&nbsp;Webs<br>co&#110;di<br>V&#105;si&#116;<br>trols<br>you u<br>s wr&#105;<br><br><br><b>&#82;ICT&#73;</b><br><br>&nbsp;li&#99;e<br>ors i<br>a&#119;&#108;&#101;r<br>compi<br><br><br>nside<br>ddres<br>at &#101;a<br>o&#117; fu<br>&#115;e<span style=\"color:#FFF;\">g</span>ad<br>al co<br>ses i<br><br><br>&nbsp;<b>&#76;AW<span style=\"color:#FFF;\">f</span></b><br><br>tion <br>h &#111;r <br>ed b&#121;<br>ct (t<br>&nbsp;betw<br>dmin <br>th&#101; A<br>onnec<br>servi<br><br><br><b>V&#73;SIT</b><br><br>&nbsp;Prot<br>the<span style=\"color:#FFF;\">c</span>\"<br>atc&#104;&#101;<br>ess f<br><br>HERIN<br>&#84;&#72;E I<br>&nbsp;S&#69;RV<br></font></td>\n<td><font face=\"courier\"><b>DITIO</b><br><br>&nbsp;agre<br>ns. T<br>ite. <br>&#116;ions<br>or&#115; t<br>&#44; aut<br>n&#100;&#101;&#114; <br>tten <br><br><br><b>ONS</b>&nbsp;<b>F</b><br><br>&#110;se t<br>nc&#108;ud<br>s, &#104;a<br>le or<br><br><br>red<span style=\"color:#FFF;\">a</span>p<br>ses a<br>&#99;h em<br>rthe&#114;<br>dress<br>l&#108;ect<br>s rec<br><br><br><b>AND</b>&nbsp;<b>J</b><br><br>or pr<br>arisi<br>&nbsp;the <br>he \"A<br>&#101;en A<br>S&#116;ate<br>dmin <br>tion <br>c&#101; of<br><br><br><b>OR</b>&nbsp;<b>&#85;S</b><br><br>oco&#108; <br>I&#100;ent<br>d &#116;o <br>&#111;&#114;<span style=\"color:#FFF;\">f</span>a&#110;<br><br>G, &#83;T<br>DEN&#84;I<br>ICE.<br></font></td>\n<td><font face=\"courier\"><b>NS</b>&nbsp;<b>O&#70;</b><br><br>&#101;ment<br>hes&#101;<span style=\"color:#FFF;\">e</span><br>By &#118;i<br>&nbsp;(th&#101;<br>o the<br>hors <br>the T<br>perm&#105;<br><br><br><b>OR</b>&nbsp;<b>NO</b><br><br>o<span style=\"color:#FFF;\">t</span>&#97;cc<br>&#101;, bu<br>rvest<br>&nbsp;&#103;ath<br><br><br>ropri<br>re &#112;r<br>&#97;il a<br>&nbsp;ag&#114;e<br>es su<br>ion, <br>o&#103;niz<br><br><br><b>&#85;&#82;ISD</b><br><br>oceed<br>ng fr<br>la&#119; o<br>dmin <br>&#100;min <br>. Yo&#117;<br>State<br>with <br>&nbsp;pro&#99;<br><br><br><b>&#69;<span style=\"color:#FFF;\">i</span>AN&#68;</b><br><br>add&#114;e<br>i&#102;i&#101;r<br>your <br>y rea<br><br>&#79;RING<br>FI&#69;R <br><br></font></td>\n<td><font face=\"courier\">&nbsp;<b>USE</b>&nbsp;<br><br>&nbsp;(\"th<br>terms<br>si&#116;&#105;n<br><span style=\"color:#FFF;\">i</span>\"&#84;er<br>&nbsp;&#87;ebs<br>o&#114; ot<br>erms <br>&#115;s&#105;&#111;n<br><br><br><b>N-HU&#77;</b><br><br>ess t<br>t<span style=\"color:#FFF;\">o</span>ar&#101;<br>ers,<span style=\"color:#FFF;\">h</span><br>er co<br><br><br>etary<br>ovide<br>ddres<br>&#101; tha<br>bs&#116;an<br>harve<br>ed &#97;s<br><br><br><b>ICTIO</b><br><br>ing b<br>om th<br>f the<br>State<br>S&#116;ate<br>&nbsp;co&#110;s<br>. Y&#111;&#117;<br>&#98;reac<br>e&#115;s r<br><br><br>&nbsp;<b>A&#66;US</b><br><br>ss re<br>\") &#105;&#102;<br>Int&#101;r<br>son&#46;<br><br>, &#84;RA<br>CONST<br><br></font></td>\n<td><font face=\"courier\"><br><br>e W&#101;b<br>&nbsp;are <br>&#103; (in<br>ms of<br>ite s<br>herwi<br>of Se<br>&nbsp;&#111;f t<br><br><br><b>AN</b>&nbsp;<b>VI</b><br><br>he We<br>&nbsp;&#110;o&#116; <br>or an<br>ntent<br><br><br>&nbsp;inte<br>&#100;<span style=\"color:#FFF;\">p</span>for<br>s th&#101;<br>t the<br>tia&#108;l<br>sting<br>&nbsp;a vi<br><br><br><b>N</b>&nbsp;<br><br>rough<br>e Ter<br>&nbsp;stat<br>\") fo<br>&nbsp;resi<br>e&#110;t t<br>&nbsp;c&#111;ns<br>&#104;&#101;s<span style=\"color:#FFF;\">d</span>o<br>egard<br><br><br><b>E</b>&nbsp;<br><br>c&#111;rde<br>&nbsp;we s<br>net P<br><br><br>NSFER<br>ITUT&#69;<br><br></font></td>\n<td><font face=\"courier\"><br><br>site\"<br>in ad<br>&nbsp;&#97;&#110;y <br>&nbsp;Se&#114;v<br>hall <br>se<span style=\"color:#FFF;\">d</span>ma<br>r&#118;ice<br>he ow<br><br><br><b>SITOR</b><br><br>bsite<br>lim&#105;t<br>y o&#116;h<br>&nbsp;fr&#111;m<br><br><br>llec&#116;<br>&nbsp;huma<br>&nbsp;Webs<br><span style=\"color:#FFF;\">p</span>comp<br>y dim<br>, g&#97;t<br>olati<br><br><br><br><br>t b&#121; <br>ms<span style=\"color:#FFF;\">t</span>of<br>&#101; o&#102; <br>r t&#104;e<br>&#100;&#101;nts<br>o the<br>ent t<br>f the<br>ing a<br><br><br><br><br>d. &#65;n<br>&#117;&#115;pec<br>rotoc<br><br><br>RING <br>S A&#78; <br><br></font></td>\n<td><font face=\"courier\"><br><br>) &#105;s <br>ditio<br>manne<br>&#105;&#99;&#101;\")<br>be co<br>k&#101;s u<br>&nbsp;are<br>ner &#111;<br><br><br><b>S</b>&nbsp;<br><br>&nbsp;&#97;ppl<br>ed<span style=\"color:#FFF;\">c</span>to<br>er co<br>&nbsp;th&#101; <br><br><br>ual p<br>n v&#105;s<br>i&#116;e c<br>ilati<br>inish<br>&#104;erin<br>on<span style=\"color:#FFF;\">p</span>of<br><br><br><br><br>such <br>&nbsp;Serv<br>resid<br>&nbsp;Webs<br><span style=\"color:#FFF;\">i</span>&#101;nte<br><span style=\"color:#FFF;\">t</span>ju&#114;i<br>&#111; the<br>se<span style=\"color:#FFF;\">a</span>Te<br>&#99;tion<br><br><br><br><br>&nbsp;&#101;mai<br>t<span style=\"color:#FFF;\">a</span>pot<br>ol a&#100;<br><br><br>TO A<span style=\"color:#FFF;\">i</span><br>ACC&#69;&#80;<br><br></font></td>\n<td><font face=\"courier\"><br><br>&#112;rovi<br>n to <br>r) t&#104;<br>. &#80;le<br>&#110;side<br>se &#111;f<br><br>f<span style=\"color:#FFF;\">e</span>the<br><br><br><br><br>y to<br>,<span style=\"color:#FFF;\">t</span>w&#101;b<br>mpute<br>Web&#115;&#105;<br><br><br>r&#111;per<br>itors<br>ontai<br>&#111;n,<br>es th<br>g, an<br>&nbsp;this<br><br><br><br><br>party<br>ice<br>&#101;nce <br>ite a<br>red i<br>sdict<br>&nbsp;venu<br>rms o<br>&#115; und<br><br><br><br><br>l add<br>&#101;ntia<br>dress<br><br><br>THIRD<br>TANCE<br><br></font></td>\n<td><font face=\"courier\"><br><br>ded<br>&#97;&#110;y<br>&#101;<br>ase<br>red<br><br><br><br><br><br><br><br><br><br>r<br>te<br><br><br>ty.<br><br>ns<br><br>e<br>d/or<br><br><br><br><br><br><br><br>of<br>s<br>&#110;to<br>ion<br>e &#105;n<br>f<br>er<br><br><br><br><br>re&#115;s<br>l<br>&#46;<br><br><br><br>&nbsp;AND<br><br></font></td>\n</tr>\n</table>\n<br>"; }

        static readonly Regex _Cleaner = CleanerRegex();
        static readonly char[] _splitChars = ['='];
        readonly string ForcedScriptFile = configuration.GetSection("ProjectHoneyPot").GetValue<string>("ScriptSource");
        readonly string ForcedScriptName = configuration.GetSection("ProjectHoneyPot").GetValue<string>("ScriptName");
        readonly string ForcedScriptUri = configuration.GetSection("ProjectHoneyPot").GetValue<string>("ScriptUri");
        readonly string ForcedIP = configuration.GetSection("ProjectHoneyPot").GetValue<string>("MyIP");
        string[] Directives;
        Dictionary<string, string> Settings;

        [Route(BASE_PATH + "attendant")]
        public async Task Index()
        {
            Dictionary<string, string> post = PrepareRequest();

            StringBuilder RequestText = new StringBuilder();
            foreach (KeyValuePair<string, string> kv in post)
            {
                if (RequestText.Length > 0)
                {
                    RequestText.Append('&');
                }
                RequestText.Append(kv.Key).Append('=').Append(kv.Value);
            }
            string ResponseText = await PerformRequest(RequestText.ToString());
            Settings = TranscribeResponse(ResponseText);
            if (Settings.TryGetValue("directives", out string value))
            {
                Directives = value.Split(',');
            }

            Response.Headers.CacheControl = "no-store, no-cache, must-revalidate, max-age=0";
            Response.Headers.Pragma = "no-cache";
            Response.Headers.Expires = "0";

            await WritePageContent();
        }

        async Task WritePageContent()
        {
            if (Directive(0) == "1") await Response.WriteAsync(DOC_TYPE1);
            await WriteIfSet("injDocType");
            if (Directive(1) == "1") await Response.WriteAsync(HEAD1);
            await WriteIfSet("injHead1HTML");
            if (Directive(8) == "1") await Response.WriteAsync(ROBOT1);
            await WriteIfSet("injRobotHTML");
            if (Directive(9) == "1") await Response.WriteAsync(NOCOLLECT1);
            await WriteIfSet("injNoCollectHTML");
            if (Directive(1) == "1") await Response.WriteAsync(HEAD2);
            await WriteIfSet("injHead2HTML");
            if (Directive(2) == "1") await Response.WriteAsync(TOP1);
            await WriteIfSet("injTopHTML");
            await WriteIfOn("actMsg");
            await WriteIfOn("errMsg");
            await WriteIfOn("customMsg");
            if (Directive(3) == "1") await Response.WriteAsync(LEGAL1 + GetLegalContent() + LEGAL2);
            await WriteIfSet("injLegalHTML");
            await WriteIfOn("altLegal");
            if (Directive(4) == "1") await Response.WriteAsync(GetEmailHTML(Settings["emailmethod"], Settings["email"]));
            await WriteIfSet("injEmailHTML");
            if (Directive(5) == "1") await Response.WriteAsync(STYLE1);
            await WriteIfSet("injStyleHTML");
            if (Directive(6) == "1") await Response.WriteAsync(VANITY1);
            await WriteIfSet("injVanityHTML");
            await WriteIfOn("altVanity");
            if (Directive(7) == "1") await Response.WriteAsync(BOTTOM1);
            await WriteIfSet("injBottomHTML");
        }

        public string MapPath(string path)
        {
            if (!path.StartsWith("~/"))
                return path;

            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path.Replace("~/", ""));
        }

        public Dictionary<string, string> PrepareRequest()
        {
            var ip = Request.Headers["CF-Connecting-IP"].ToString() ?? httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? "";
            if (String.IsNullOrWhiteSpace(ip))
                ip = httpContextAccessor.HttpContext?.GetServerVariable("REMOTE_HOST") ?? "";
            Dictionary<string, string> postvars = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "tag1", HPOT_TAG1 },
                { "tag2", HPOT_TAG2 },
                { "tag3", HPOT_TAG3 },
                { "tag4", GENERATED_SOURCE_HASH },
                { "ip", HttpUtility.UrlEncode(ip) },
                { "svrn", HttpUtility.UrlEncode(httpContextAccessor.HttpContext?.GetServerVariable("SERVER_NAME")) },
                { "svp", HttpUtility.UrlEncode(httpContextAccessor.HttpContext?.GetServerVariable("SERVER_PORT")) },
                { "svip", HttpUtility.UrlEncode(ForcedIP ?? httpContextAccessor.HttpContext?.GetServerVariable("SERVER_ADDR")) },
                { "rquri", HttpUtility.UrlEncode(ForcedScriptUri ?? httpContextAccessor.HttpContext?.GetServerVariable("URL")) },
                { "sn", HttpUtility.UrlEncode(ForcedScriptName ?? httpContextAccessor.HttpContext?.GetServerVariable("SCRIPT_NAME")).Replace(" ", "%20") },
                { "ref", HttpUtility.UrlEncode(httpContextAccessor.HttpContext?.GetServerVariable("HTTP_REFERER")) },
                { "uagnt", HttpUtility.UrlEncode(httpContextAccessor.HttpContext?.GetServerVariable("HTTP_USER_AGENT")) }
            };

            if (httpContextAccessor.HttpContext?.Request.Method == "POST" && Request.Form.Count > 0)
            {
                postvars.Add("has_post", "" + Request.Form.Count);

                foreach (string key in Request.Form.Keys)
                {
                    postvars["post|" + key] = Request.Form[key];
                }
            }

            if (!string.IsNullOrEmpty(Request.QueryString.Value))
            {
                // Parse the query string into key-value pairs
                var queryCollection = Request.Query;
                postvars.Add("has_get", "" + queryCollection.Count);

                foreach (var key in queryCollection.Keys)
                {
                    postvars["get|" + key] = queryCollection[key];
                }
            }
            return postvars;
        }

        private static async Task<string> PerformRequest(string p)
        {
            using (var httpClient = new HttpClient())
            {
                var requestUri = $"http://{REQUEST_HOST}{REQUEST_SCRIPT}";
                var content = new StringContent(p, Encoding.UTF8, "application/x-www-form-urlencoded");

                var request = new HttpRequestMessage(HttpMethod.Post, requestUri)
                {
                    Content = content
                };
                request.Headers.Add("Cache-Control", "no-cache");
                request.Headers.UserAgent.ParseAdd("PHPot " + HPOT_TAG2);
                request.Headers.ConnectionClose = true;

                try
                {
                    using (var response = await httpClient.SendAsync(request))
                    {
                        response.EnsureSuccessStatusCode();
                        return await response.Content.ReadAsStringAsync();
                    }
                }
                catch (HttpRequestException hre)
                {
                    throw new Exception(hre.Message, hre);
                }
            }
        }

        private static Dictionary<string, string> TranscribeResponse(string response)
        {
            Dictionary<string, string> settings = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            string[] arr = response.Split((char)10);
            bool isParam = false;

            for (int j = 0; j < arr.Length; j++)
            {
                if (arr[j] == "<END>")
                {
                    isParam = false;
                }

                if (isParam)
                {
                    string[] pieces = arr[j].Split(_splitChars, 2);
                    if (pieces.Length == 2)
                    {
                        settings.Add(pieces[0], HttpUtility.UrlDecode(pieces[1]));
                    }
                }

                if (arr[j] == "<BEGIN>")
                {
                    isParam = true;
                }
            }
            return settings;
        }

        protected string Directive(int index)
        {
            if (Directives != null && Directives.Length > index)
            {
                return Directives[index];
            }
            return null;
        }

        protected string GetEmailHTML(string Method, string Email)
        {
            return Method switch
            {
                "0" => "",
                "1" => EMAIL1A + Email + EMAIL1B + Email + EMAIL1C,
                "2" => EMAIL2A + Email + EMAIL2B + Email + EMAIL2C,
                "3" => EMAIL3A + Email + EMAIL3B + Email + EMAIL3C,
                "4" => EMAIL4A + Email + EMAIL4B + Email + EMAIL4C,
                "5" => EMAIL5A + Email + EMAIL5B + Email + EMAIL5C,
                "6" => EMAIL6A + Email + EMAIL6B + Email + EMAIL6C,
                "7" => EMAIL7A + Email + EMAIL7B + Email + EMAIL7C,
                "8" => EMAIL8A + Email + EMAIL8B + Email + EMAIL8C,
                "9" => EMAIL9A + Email + EMAIL9B + Email + EMAIL9C,
                _ => EMAIL10A + Email + EMAIL10B + Email + EMAIL10C,
            };
        }

        protected async Task WriteIfOn(string key)
        {
            if (Settings.TryGetValue(key + "On", out string val) && !String.IsNullOrEmpty(val))
            {
                if (Settings.TryGetValue(key, out string value))
                {
                    await Response.WriteAsync(value);
                }
                else
                {
                    await Response.WriteAsync(Settings[key + "Msg"]);
                }
            }
        }

        protected async Task WriteIfSet(string key)
        {
            if (Settings.ContainsKey(key))
            {
                await Response.WriteAsync(Settings[key + "Msg"]);
            }
        }

        [GeneratedRegex("[^0-9a-zA-Z]")]
        private static partial Regex CleanerRegex();
    }
}
