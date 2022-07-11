<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="../wristwatch_selector.aspx.cs" Inherits="ErnestBorel.wristwatch_selector" %>

<!DOCTYPE HTML>
<html lang="jp">
<head>
<base href="<%=domain_ch %>/jp/" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<meta name="description" content="desc">
<meta name="keywords" content="keyword">
<title>Ernest Borel - Selector</title>
<!-- For watch detail -->
<!-- panel start -->
<!--#include file ="include.inc"-->
<!-- panel end -->
<script type="text/javascript" src="/js/search.js?v=2018"></script>
<script type="text/javascript">
    nid = 1;
    subId = 4;
    lang = "jp";
    var keyword = "<%=keyword%>";
	$(document).ready(function () {
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
            <h2>セレクター</h2>
			<div class="search">
				<div class="inner-panel">
					<div class="selectionGroup">
						<div class="selection">
							<span>タイプ</span>
							<select multiple id="sel-gender" name="sel-gender" data-placeholder="タイプを選択してください">
								<option value="'M'">メンズ</option>
								<option value="'F'">レディース</option>
								<option value="'N'">男女兼用</option>
							</select>
						</div>
						<div class="selection">
							<span>ベルト</span>
							<select multiple id="sel-bracelet" name="sel-bracelet" data-placeholder="ベルトを選択してください">
								<option value="'GOLD'">金の腕輪</option>
								<option value="'STEEL'">テンレススチールブレスレット</option>
								<option value="'LEATHER'">レザーストラップ</option>
								<option value="'NYLON'">ナイロン</option>
								<option value="'TPU'">TPU</option>
							</select>
						</div>
						<div class="selection">
							<span>ケース形状</span>
							<select multiple id="sel-shape" name="sel-shape" data-placeholder="ケース形状を選択してください">
								<option value="'ROUND'">ラウンド</option>
								<option value="'SQUARE'">スクエア</option>
								<option value="'RECTANGULAR'">矩形</option>
								<option value="'IRREGULAR'">特殊な形状</option>
							</select>
						</div>
					</div>

					<div class="selectionGroup">
						<div class="selection">
							<span>ケース材質</span>
							<select multiple id="sel-material" name="sel-material" data-placeholder="ケース材質を選択してください">
								<option value="'KGOLD'">Kゴールド</option>
								<option value="'STEEL+KGOLD'">ステンレス+ Kゴールド</option>
								<option value="'STEEL'">ステンレス鋼</option>
								<option value="'STEEL+PLATING'">ステンレススチール + メッキ</option>
							</select>
						</div>
						<div class="selection">
							<span>ダイヤル装飾</span>
							<select multiple id="sel-cover" name="sel-cover" data-placeholder="ダイヤル装飾を選択してください">
								<option value="'DIAMOND'">ダイヤモンド/ 石</option>
								<option value="'SHELL'">MOP</option>
								<option value="'SCALE'">インデックス</option>
								<option value="'SPECIAL'">特別</option>
								<option value="'SUN'">Sun</option>
								<option value="'HYDRAULIC'">Hydraulic</option>
							</select>
						</div>
						<div class="selection">
							<a href="javascript:void()" title="キャンセル" class="btn btnReset timesNRoman">キャンセル</a>
							<a href="javascript:void()" title="検索する" class="btn btnSearch timesNRoman" rel="criteria">検索する</a>
						</div>
					</div>
					
					<div class="selectionGroup right">
						<div class="selection">
							<span>ウオッチを指定して検索</span>
							<input type="text" id="keyword" name="keyword" class="txt_search" placeholder="キーワードまたはモデル番号を入力してください"><br>
							<a href="javascript:void()" title="検索する" class="btn btnSearch timesNRoman" rel="keyword">検索する</a>
						</div>
					</div>
				</div> 
			</div>

			<div class="yui3-g">
				<div class="yui3-u-3-4">
					<div class="result">
						<div id="resultCount" class="timesNRoman"><span>0</span> 個見つかりました</div>
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
            <a class="url" href='wristwatch/col_movement/col_ref/#img' title='col_name'><img src='/images/watches/col_movement/img_t.png'></a>
        </div>
        <div class='contWrapper'>
            <h3><a class='url' href='wristwatch/col_movement/col_ref/#img'><span class="col_name">col_name</span></a></h3>
            <p class='desc'>idx_watch</p>
        </div>
        <a class='more_btn url' href='wristwatch/col_movement/col_ref/#img'>もっと見る <span class='nav_arrow'>&#9658;</span></a>
    </div>
</div>
</body>
</html>
