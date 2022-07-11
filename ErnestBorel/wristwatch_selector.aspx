<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wristwatch_selector.aspx.cs" Inherits="ErnestBorel.wristwatch_selector" %>

<!DOCTYPE HTML>
<html>
<head>
<base href="<%=domain_cn %>/" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<meta name="description" content="desc">
<meta name="keywords" content="keyword">
<title>瑞士手表品牌—依波路表官方网站</title>
<!-- For watch detail -->
<!-- panel start -->
<!--#include file ="include.inc"-->
<!-- panel end -->
<script type="text/javascript" src="/js/search.js?v=2018"></script>
<script type="text/javascript">
	nid = 1;
	subId = 4;
	lang = "sc";
	var keyword = "<%=keyword%>";
	$(document).ready(function(){
		$(".watch-menu .w1").addClass("selected");
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
		<div class="main-content norepeatbg" id="watchSelector">
			<section>
            <h2>选择器</h2>
			<div class="search">
				<div class="inner-panel">
					<div class="selectionGroup">
						<div class="selection">
							<span>性別</span>
							<select multiple id="sel-gender" name="sel-gender" data-placeholder="请选择性别">
								<option value="'M'">男裝</option>
								<option value="'F'">女裝</option>
								<option value="'N'">中裝</option>
							</select>
						</div>
						<div class="selection">
							<span>表带材料</span>
							<select multiple id="sel-bracelet" name="sel-bracelet" data-placeholder="请选择表带材料">
								<option value="'GOLD'">包金表带</option>
								<option value="'STEEL'">不锈钢表带</option>
								<option value="'LEATHER'">真皮表带</option>
								<option value="'NYLON'">尼龙织带</option>
								<option value="'TPU'">胶带</option>
							</select>
						</div>
						<div class="selection">
							<span>表面形状</span>
							<select multiple id="sel-shape" name="sel-shape" data-placeholder="请选择表面形状">
								<option value="'ROUND'">圆形</option>
								<option value="'SQUARE'">方形</option>
								<option value="'RECTANGULAR'">长方形</option>
								<option value="'IRREGULAR'">特別形狀</option>
							</select>
						</div>
					</div>
					<div class="selectionGroup">
						<div class="selection">
							<span>表壳材料</span>
							<select multiple id="sel-material" name="sel-material" data-placeholder="请选择表壳材料">
								<option value="'KGOLD'">K金表殼</option>
								<option value="'STEEL+KGOLD'">不锈钢 + K金表壳</option>
								<option value="'STEEL'">不锈钢表壳</option>
								<option value="'STEEL+PLATING'">不锈钢 + 电镀色壳</option>
							</select>
						</div>
						<div class="selection">
							<span>表面材质</span>
							<select multiple id="sel-cover" name="sel-cover" data-placeholder="请选择表面材质">
								<option value="'DIAMOND'">镶钻表面</option>
								<option value="'SHELL'">贝壳表面</option>
								<option value="'SCALE'">刻度表面</option>
								<option value="'SPECIAL'">特别表针</option>
								<option value="'SUN'">太阳纹</option>
								<option value="'HYDRAULIC'">油压表面</option>
							</select>
						</div>
						<div class="selection">
							<a href="javascript:void(0)" title="重设" class="btn btnReset timesNRoman">重设</a>
							<a href="javascript:void(0)" title="搜寻" class="btn btnSearch timesNRoman" rel="criteria">搜寻</a>
						</div>
					</div>
					
					<div class="selectionGroup right">
						<div class="selection">
							<span>搜寻指定表款</span>
							<input type="text" id="keyword" name="keyword" class="txt_search" placeholder="输入关键字或产品型号"><br>
							<a href="javascript:void(0)" title="搜寻" class="btn btnSearch timesNRoman" rel="keyword">搜寻</a>
						</div>
					</div>
				</div> 
			</div>

			<div class="yui3-g">
				<div class="yui3-u-3-4">
					<div class="result">
						<div id="resultCount" class="timesNRoman"><span>0</span> 个搜寻结果</div>
						<div id="resultList"></div>
						<div id='pagination' class="right"></div>
					</div>
				</div>
			</div>
			</section>
		</div>
		<!-- footer start -->
		<!--#include file ="footer.inc"-->
		<!-- footer end -->
	</div>
	</div>
</div>

<div class="hide">
	<div id='resultCell' class="resultblock">
        <div class='imgWrapper'>
            <a class="url" href='/wristwatch/col_movement/col_ref/#img' title='col_name'><img src='/images/watches/col_movement/img_t.png'></a>
        </div>
        <div class='contWrapper'>
            <h3><a class='url' href='/wristwatch/col_movement/col_ref/#img'><span class="col_name">col_name</span></a></h3>
            <p class='desc'>idx_watch</p>
        </div>
        <a class='more_btn url' href='/wristwatch/col_movement/col_ref/#img'>更多 <span class='nav_arrow'>&#9658;</span></a>
    </div>
</div>
</body>
</html>
