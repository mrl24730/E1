using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ErnestBorel
{

    public class OrderEmailTemplate
    {
        public string fromEmail { get; set; }
        public string fromName { get; set; }
        public string subject { get; set; }
        public string body { get; set; }

        public string itemHtml { get; set; }
        public string templateHtml { get; set; }

        public OrderEmailTemplate()
        {
            fromEmail = "order@ernestborel.ch";
            fromName = "依波路 Ernest Borel";
            subject = "Thank You For Your Order [{0}]";

            templateHtml = @"<html>
	<head>
		<meta name='viewport' content='width=device-width'>
		<style>
			@media only screen and (max-width: 750px) { .main-table{width:100%;}}
		</style>
	</head>
	<body>
		<p style='text-align: center; color: #af845d; font-size: 24px; font-weight: bold;'>[YourOrder]</p>
		<table class='main-table' width='700' align='center' cellspacing='0' cellpadding='0'>
			<tr bgcolor='#cbcbcb'>
				<td>
					<table width='100%' cellspacing='0' cellpadding='10'>
						<tr>
							<td align='left' style='color:#808080; font-weight: bold;'>[OrderNumberTxt]. [OrderNumber] </td>
							<td align='right' style='color:#808080; font-weight: bold;'>[OrderDate]</td>
						</tr>
					</table>
				</td>
			</tr>
			[ItemList]
			<tr bgcolor='#FFF'>
				<td><img src='http://www.ernestborel.cn/images/trans.gif' alt='' height='5' width='5'> </td>
			</tr>
			<tr bgcolor='#FFF'>
				<td>
					<table width='100%' cellspacing='0' cellpadding='10'>
						<tr>
							<td colspan='2'><span style='color:#000'>[OrderSummary]</span></td>
						</tr>
						<tr style='font-weight: bold; color: #808080'>
							<td align='left'>[TotalPriceTxt]</td>
							<td align='right'>[TotalPrice]</td>
						</tr>
						<tr style='font-weight: bold; color: #808080'>
							<td align='left'>[TotalQtyTxt]</td>
							<td align='right'>[TotalQty]</td>
						</tr>
						<tr style='font-weight: bold; color: #808080'>
							<td align='left' valign='top'>[DiscountTxt]</td>
                            <td align='right'>[Discount]</td>
						</tr>
					</table>
					<hr>
					<table width='100%' cellspacing='0' cellpadding='10'>
						<tr style='font-weight: bold; color: #808080' valign='top'>
							<td><span style='color:#000'>[DiscountTotalTxt]</span></td>
							<td align='right'><p align='right' style='font-size: 36px; font-weight: bold;color: #000'>[DiscountPrice]</p></td>
						</tr>
					</table>					
					<p style='color:#808080;'>* [Remark]</p>
				</td>
			</tr>
		</table>
	</body>
</html>";


            itemHtml = @"<tr bgcolor='#FFF'>
				<td><img src='http://www.ernestborel.cn/images/trans.gif' alt='' height='5' width='5'> </td>
			</tr>
			<tr bgcolor='#f0f0f0'>
				<td>
					<table width='100%' cellspacing='0' cellpadding='10'>
						<tr>
							<td width='40%' align='right'>
								<img src='[ImgPath]' alt='' height='180' width='180'>
							</td>
							<td style='color:#808080' align='left'>
								[CollectionName]<br>
								[ModelNumber]<br>
								[WatchType] - [Gender]<br>
								[UnitPrice]<br>
								[Qty]<br>
								[SubTotal]<br>
							</td>
						</tr>
					</table>
				</td>
			</tr>";
        }
    }
}