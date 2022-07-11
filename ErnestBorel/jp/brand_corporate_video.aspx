<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="../brand_corporate_video.aspx.cs" Inherits="ErnestBorel.brand_corporate_video" %>

<!DOCTYPE HTML>
<html lang="jp">
<head>
<base href="<%=domain_ch %>/jp" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<meta name="description" content="1856年，年轻的朱尔斯-波莱尔凭着缔造完善经典的信念，开始了他漫长的钟表制造生涯。1859年，他携妻弟保罗高华士创立了瑞士手表品牌—依波路。">
<meta name="keywords" content="依波路,瑞士名表,自动手表,石英手表,计时码表,奢华腕表,高端品牌,腕表, 纽察图,瑞士,依波路简介">
<title>Ernest Borel - ブランド紹介ショートムービー</title>
<!-- panel start -->
<!--#include file ="include.inc"-->
<!-- panel end -->
<script>
    nid = 2;
    $(document).ready(function () {
        //for video 
        jw_tvc = jwplayer("video-cnt").setup({
            'flashplayer': 'player.swf',
            'id': 's1-video child',
            'width': 404,
            'height': 226,
            'file': "/video/2010_MV.mp4",
            'controlbar.idlehide': true,
            modes: [
               { type: "html5" },
               { type: "flash", src: "player.swf" }
            ]
        });

        jw_tvc.play();

    })
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
		<div class="main-content theBrand">
		<section>
				<h2>ブランドの歴史</h2>
				<h3>ブランド紹介ショートムービー</h3>

				<div class="yui3-g">
				<div>
					<div class="yui3-u-1-2">
						<div id="video-cnt"></div>
					</div>
					<div class="yui3-u-1-2">
					<div class="person_cont">
						<div class="person">
							<img src="/images/foundeteur.jpg" alt="アーネスト・ボレルの創始者">
							<p>M.JULES.BOREL</p>
							<P>アーネスト・ボレルの創始者</P>
						</div>
						<div class="person">
							<img src="/images/president.jpg" alt="二代目のトップ指導者">
							<p>M.ERNEST BOREL</p>
							<p>二代目のトップ指導者</p>
						</div>
						<div class="person">
							<img src="/images/direteur.jpg" alt="三代目のトップ指導者">
							<p>M.JEAN-LOUIS BOREL</p>
							<P>三代目のトップ指導者</P>
						</div>
						</div>
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
</body>
</html>
