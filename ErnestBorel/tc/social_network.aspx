<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="../social_network.aspx.cs" Inherits="ErnestBorel.social_network" %>

<!DOCTYPE HTML>
<html lang="tc">
<head>
<base href="<%=domain_ch %>/tc/" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<meta name="description" content="一個百年品牌—依波路,因一段美麗浪漫的傳奇歷史,揭開了她更加精彩華美篇章,也許愛情這東西都是從天而降的吧,浪漫,因為相遇這種奇遇每天都在發生,但總該有一樣東西,用來紀念這樣美麗的奇蹟:戀上依波路手表">
<meta name="keywords" content="依波路,瑞士名表,自動手表,石英手表,計時碼表,奢華腕表,高端品牌,腕表, 紐察圖,瑞士,依波路品牌歷史">
<title>依波路手表品牌歷史_依波路手表官網</title>


<script type="text/javascript" src="/js/jquery/jquery-1.9.1.min.js"></script>
<!-- panel start -->
<!--#include file ="include.inc"-->
<!-- panel end -->
<script type="text/javascript" src="/js/masonry.pkgd.min.js"></script>
<script type="text/javascript" src="/js/imagesloaded.pkgd.min.js"></script>
<script src="/js/instagram.class.js" type="text/javascript"></script>
<script type="text/javascript"> 
	nid = 3; subId = 5;
	window.instagram = new instagramClass();
    $(document).ready(function(){
    	instagram.initUI($("#gallery-wrap"));
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
		<div class="main-content" id="socialNetwork">
		<section>
				<h2>照片分享</h2>
				<p>分享你的動人/精彩/浪漫時刻，立即上載你的相片到 Instagram　#ernestborel</p>
				<div id="gallery-wrap" class="clearfix masonry"></div>
				<div class="loadingWrapper">
					<a href="javascript:void(0);" class="btn_more">More</a>
					<!-- <img src="images/gallery_loading.gif" class="loadingSpin" width="30"> -->
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
