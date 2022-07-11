<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="../social_network.aspx.cs" Inherits="ErnestBorel.social_network" %>

<!DOCTYPE HTML>
<html lang="jp">
<head>
<base href="<%=domain_ch %>/jp/" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<meta name="description" content="一个百年品牌—依波路,因一段美丽浪漫的传奇历史,揭开了她更加精彩华美篇章,也许爱情这东西都是从天而降的吧,浪漫,因为相遇这种奇遇每天都在发生,但总该有一样东西,用来纪念这样美丽的奇迹:恋上依波路手表">
<meta name="keywords" content="依波路,瑞士名表,自动手表,石英手表,计时码表,奢华腕表,高端品牌,腕表, 纽察图,瑞士,依波路品牌历史">
<title>Ernest Borel - #ERNESTBOREL</title>


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
    $(document).ready(function () {
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
				<h2>写真共有</h2>
				<p>自分だけの感動的で、すばらしくロマンティックなひとときをみんなと共有、画像を今すぐInstagram　#ernestborel　にアップしましょう。</p>
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
