<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="../tech_info_terminology.aspx.cs" Inherits="ErnestBorel.tech_info_terminology" %>

<!DOCTYPE HTML>
<html lang="jp">
<head>
<base href="<%=domain_ch %>/jp/" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<meta name="description" content= "瑞士依波路手表类型及常用术语介绍，机械表主要由以下几个部分组成：(1)动力机构，即主发条及上条机构; (2)传动轮系;(3)调速机构，即擒纵系统和摆轮元件;(4)指针和校时机构……" >
<meta name="keywords" content="依波路,瑞士名表,自动手表,石英手表,计时码表,奢华腕表,高端品牌,腕表, 纽察图,瑞士,依波路使用须知">
<title>Ernest Borel - 技術情報</title>
<!-- panel start -->
<!--#include file ="include.inc"-->
<!-- panel end -->
<script type="text/javascript" src="/js/jquery/jquery.scrollfollow.modForJq164.js"></script>
<script type="text/javascript" src="/js/jquery/jquery.ui.core.js" ></script>
<script type="text/javascript" src="/js/jquery/jquery.ui.widget.js" ></script>
<script type="text/javascript" src="/js/jquery/jquery.ui.accordion.js" ></script>
<script>
    nid = 1;
    subId = 3;
    $(document).ready(function () {
        $(".watch-menu .w3").addClass("selected");

        //accordion, float index
        /*
        $("#floatCont").height($(".yui3-u-3-4 .content").height())
        $(".floatbox").scrollFollow({container:'termfloat',speed:1150});
        */

        $(".termIndex").accordion({
            autoHeight: false,
            navigation: true
        });
    });
</script>
</head>

<body>
<!-- panel start -->
<!--#include file ="panel.inc"-->
<!-- panel end -->
<div id="doc">
	<div id="docShadow">
	<div id="docWrapper">
		<!-- header start -->
        <!--#include file ="header.inc"-->
		<!-- header end -->
		
		<!-- nav start -->
		<!--#include file ="nav.inc"-->
		<!-- nav end -->
		<div class="main-content" id="tech_info">
		<section>
				<h2>技術情報</h2>
				<h3>專門用語 | <a href="tech_info_instruction">取扱說明書</a></h3>
				<div class="yui3-g">
					<div class="yui3-u-3-4">
						<div class="content">
							<div id="techinfo_term">
									<ol class="instruct">
										<li>
																				<h5><a name="Mechanical Watches"></a>機械式時計（mechanical watch）</h5>
																			メ機械式時計は、次の四つの部分から構成されています。①動力機構（ゼンマイ、巻上げ機構）、②ギアトレイン、③調速機構（脱進機、テン輪機構）、④指針と校正機構。機械式時計には手巻き式と自動巻きがあり、いずれもゼンマイ仕掛けの動力機構です。手巻き式の場合、リューズを手で回してゼンマイを巻き上げる方式であり、自動巻きは、腕時計を着用時における腕の自然な振りや動きで巻き上げ機構を駆動させます。</li>
																		<li>
																				<h5><a name="Quartz Watches"></a>クォーツ時計（quartz watch）</h5>
																				クォーツ時計には、指針式、デジタル式、または両者の混合タイプに分かれます。電池のエネルギーと水晶の振動を用いて時を刻みます。指針式は電池を使って水晶を振動させてギアトレインを駆動させる方式であり、指針を回すステップモーターはIC基板がコントロールします。デジタルクォーツ時計は、時刻が液晶基板に数字で表示されます。
																		</li>
																		<li>
																				<h5><a name="Chronometer"></a>クロノメーター（chronometer）</h5>
																				クロノメーターは、精密度、正確度の高い腕時計です。さまざまな角度や温度での試験にパスしたことを証するスイス公認のCOSC証書を取得する必要があります。　
																		</li>
																		<li>
																				<h5><a name="Multifunction Watches"></a>多機能ウオッチ（multifunction watch）</h5>
																				多機能ウオッチとは、クロノグラフ、第二のタイムゾーン、アラーム、永久カレンダー、ミニッツリピーターなどの機能が付加された腕時計のこと。		
																		</li>
																		<li>
																				<h5><a name="Grande Complication"></a>グランドコンプリケーション（grande complication）</h5>
																				グランドコンプリケーションとは、少なくとも、永久カレンダー、ミニッツリピーターおよびクロノグラフ（ダブルスプリットセコンド）またはトゥールビヨンの機能を持つ腕時計のこと。		
																		</li>
																		<li>
																				<h5><a name="Minute Repeater"></a>ミニッツリピーター（minute repeater）</h5>
																				原義は、時刻の分を知らせること。横スライド式のゼンマイをまわすと、時間と分を音で知らせる機能を持つ腕時計です。
																		</li>
																		<li>
																				<h5><a name="Chronograph"></a>クロノグラフ（chronogragh）</h5>
																				ストップウオッチ機能（計時、停止およびリセットの機能）をもつ腕時計または懐中時計のこと。		
																		</li>
																		<li>
																				<h5><a name="Musical Watch"></a>ミュージカル・ウオッチ（musical watch）</h5>
																				音楽による時報を告げることができる機械式腕時計のこと。ムーブメントにはディスクタイプとドラムタイプがあります。		
																		</li>
																		<li>
																				<h5><a name="Water Resistance"></a>防水（water resistance）</h5>
																				防水時計ならば、ケース、リューズ、クリスタルフェースが防水、防塵で、且つ特定の水深まで防水性が維持されなければなりません。防水性は定期的な検査が必要です。
																		</li>
																		<li>
																				<h5><a name="Jumping Hours (or single-eyed)"></a>ジャンピングアワー（別名、シングルアイ）（jumping hours （or single-eyed））</h5>
																				針ではなく1から12までの数字の文字盤で時間を表します。文字盤は一時間ごとに数字がひとつ前に飛ぶ、つまり一時間経過するごとに数字が切り替わります。文字盤には数字を示す小窓が付いています。
																		</li>
																		<li>
																				<h5><a name="Antimagnetic Watches"></a>耐磁時計（antimagnetic watch）</h5>
																				テン輪のゼンマイが非磁性のニッケル合金でできていて、磁界の影響を受けない腕時計のこと。		
																		</li>
																		<li>
																				<h5><a name="Diver's Watch"></a>ダイバーズウオッチ（diver’s watch）</h5>
																				水深200メートルまで潜れるよう、ねじ込み式リューズと20ATM耐性を備えた、ダイバー用特別設計ウオッチのこと。
																		</li>
																		<li>
																				<h5><a name="Tourbillon"></a>トゥールビヨン（tourbillon）</h5>
																				脱進調速機を回転させる機械式時計のこと。重力（つまり、姿勢差）の影響を排し、ムーブメントの正確さを維持できるのが特徴。通常の機械式時計がもつ脱進調速機の機能はすべて備えているのに加えて、トゥールビヨンでは脱進調速機が軸心をめぐって360度回転を続けて機構の位置を絶えず調整するとともに、重力による計時誤差を補正します。
																		</li>
																		<li>
																				<h5><a name="Rhodium Plating"></a>ロジウムメッキ（rhodium plating）</h5>
																				ロジウムは白銀色の上品な貴金属です。ロジウムメッキの工程は複雑ですが、硬いので磨耗に強く、耐食性と化学安定性にすぐれ、表面につやを与えることから、製品の信頼性を向上させ、使用寿命を延ばすことができます。
																		</li>
																		<li>
																				<h5><a name="Geneva Seal"></a>ジュネーブシール（Geneva Seal）</h5>
																				「ジュネーブシール」は、腕時計の製造基準を整備するため、1886年12月6日に採択された制度です。1975年に更新されて11条のルールが定められ、1994年12月22日において最終的な見直しが行われて12条のルールが適用されました。強制ではありませんが、ガイドラインに適合すると認められた時計は、ムーブメントに”鷹と鍵”の「ジュネーブシール」紋章が刻印されているので、簡単に見分けがつきます。　
																		</li>
																		<li>
																				<h5><a name="Geneva Stripes"></a>ジュネーブストライプ（Geneva stripes）</h5>
																				ロジウムメッキする前に、ムーブメントのプレートとブリッジに刻まれる、上品な平行のしま模様のこと。ジュネーブストライプが刻まれるのは通常、高級腕時計のみです。
																		</li>
																		<li>
																				<h5><a name="Moon Phase"></a>ムーンフェーズ（moon phase watch）</h5>
																				腕時計のムーンフェーズは、ムーブメントに組み込まれたムーンディスクを回転させることにより、文字盤にある半円形の小窓を通して月齢（新月、下弦、上弦、満月）を表示する機能のことです。月の満ち欠けの一サイクルは、29日プラス12時間44分3秒。ムーンディスクには二つの月が向き合って描かれており、59の歯をもつギアホイールが59日かけて、つまり月の満ち欠け二サイクルごとに一回転して月齢を表示します。 		
																		</li>
																		<li>
																				<h5><a name="Greenwich Mean Time (GMT)"></a>グリニッジ標準時（GMT）</h5>
																				GMT は「Greenwich Mean Time」の略語であり、グリニッジ標準時（Universal Time、略称UT）としても知られています。イギリスのグリニッジ王立天文台が置かれている場所であり、世界の経度の基準でもあります。		
																		</li>
									</ol>
								</div>
							</div>
						</div>
								
						<!-- floating index start -->
						<!--
						<div class="yui3-u-1-4">
							<div id="floatCont" class="termfloat">
								<div class="floatbox">
									<div class="sideBox termIndex">
										<h5>三划</h5>
										<ul class="capital_A">
											<li><a href="#Grande Complication">大复杂表</a></li>
											<li><a href="#Minute Repeater">三问表</a></li>
										</ul>
										<h5>四划</h5>
										<ul class="capital_A">
											<li><a href="#Chronometer">天文台表</a></li>
											<li><a href="#Chronograph">计时码表</a></li>
											<li><a href="#Geneva Seal">日内瓦印记</a></li>
											<li><a href="#Geneva Stripes">日内瓦条纹</a></li>
											<li><a href="#Moon Phase">月相</a></li>
										</ul>
										<h5>五划</h5>
										<ul class="capital_A">
											<li><a href="#Quartz Watches">石英表</a></li>
										</ul>
										<h5>六划</h5>
										<ul class="capital_A">
											<li><a href="#Mechanical Watches">机械表</a></li>
											<li><a href="#Multifunction Watches">多功能表</a></li>
										</ul>
										<h5>七划</h5>
										<ul class="capital_A">
											<li><a href="#Tourbillon">陀飞轮</a></li>
											<li><a href="#Water Resistance">防水表</a></li>
											<li><a href="#Antimagnetic Watches">防磁表</a></li>
											<li><a href="#Water Resistance">防水表</a></li>
										</ul>
										<h5>十划</h5>
										<ul class="capital_A">
											<li><a href="#Greenwich Mean Time (GMT)">格林威治平均时间(GMT)</a></li>
										</ul>
										<h5>十三划</h5>
										<ul class="capital_A">
											<li><a href="#Jumping Hours (or single-eyed)">跳时表 (或称为独眼龙)</a></li>
										</ul>
										<h5>十四划</h5>
										<ul class="capital_A">
											<li><a href="#Rhodium Plating">镀铑处理</a></li>
										</ul>
										<h5>十五划</h5>
										<ul class="capital_A">
											<li><a href="#Diver's Watch">潜水表</a></li>
										</ul>
								</div>
								<a class="totop" href="#"></a> </div>
							</div>
						</div>
						-->
						<!-- floating index end -->
					</div>
				</section>

		</div>
		<!-- footer start -->
        <!--#include file ="footer.inc"-->
		<!-- footer end -->
	</div>
	</div> 
</div>
</body>
</html>
