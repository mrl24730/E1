package  {
	
	import flash.display.MovieClip;
	import flash.display.LoaderInfo;
	import com.adobe.serialization.json.*;
	import fl.video.FLVPlayback;
	import flash.display.Loader;
	import flash.net.URLRequest;
	import flash.events.MouseEvent;
	import flash.events.Event;

	//import com.kitchendigital.utils.misc;
	
	
	public class player extends MovieClip {
		
		
		public function player() {
			stage.scaleMode = "noScale";
			stage.align = "left";

			var paramObj:Object = LoaderInfo(root.loaderInfo).parameters;
			//paramObj.config='{"clip": {"url": "../video/tvc.mp4", "autoPlay":false, "autoBuffering":true, "poster":"http://202.85.164.80/canon_pixma_besmarthome_v3/images/clicktoplay.jpg"}}';
			//chklog.appendText("\n paramObj:"+paramObj);
			if(paramObj["config"]){

				
				var flashVars = JSON.decode(paramObj["config"]);
				//chklog.text = flashVars;
				//misc.traceObject(flashVars,4,chklog);
				if(flashVars.clip.url){
					//var vPlayer:FLVPlayback = new FLVPlayback();
					vPlayer.width = stage.stageWidth;
					vPlayer.height = stage.stageHeight;
					
					//chklog.text = flashVars.clip.url;
					//trace(flashVars.clip.url);
					/*
					vPlayer.skin = "swf/SkinOverAllNoFullNoCaption.swf";
					vPlayer.skinBackgroundColor = 0x666666;
					vPlayer.skinBackgroundAlpha = .8;
					vPlayer.skinAutoHide = true;
					vPlayer.skinScaleMaximum = 1200;
					*/
					vPlayer.source = flashVars.clip.url;
					
					if(Boolean(flashVars.clip.autoPlay)){
						vPlayer.play();
					}
						
					if(flashVars.clip.autoBuffering){
						vPlayer.bufferTime = 1;
					}else{
						vPlayer.bufferTime = 0;
					}
					//hin's edit
					if(flashVars.clip.poster){
						//trace(flashVars.clip.poster);
						var imageLoader:Loader = new Loader();
						imageLoader.contentLoaderInfo.addEventListener(Event.INIT,initHandler);
						var imageReq:URLRequest = new URLRequest(flashVars.clip.poster);
						imageLoader.load(imageReq);
						var _mc1:MovieClip;						
						
						function initHandler(){
							_mc1 =new MovieClip();
							vPlayer.visible = false;
							_mc1.addChild(imageLoader);
							_mc1.buttonMode=true;
							_mc1.addEventListener(MouseEvent.CLICK,showUp);
							var paddingLeft:Number = calCenterPos("width");
							var paddingTop:Number = calCenterPos("height");
							trace(paddingLeft); 
							trace(stage.width);
							trace(_mc1.width);
							imageLoader.x=paddingLeft;
							imageLoader.y=paddingTop;
							stage.addChild (_mc1);
						}
						
						function calCenterPos(d:String){
							var resultSet:Number =0;
							
							if(d=="width")
							{
								resultSet = (stage.width-_mc1.width)/2;
							}
							if(d=="height")
							{
								resultSet = (stage.height-_mc1.height)/2;
							}
							
							return resultSet; 
						}
						function showUp(){
							vPlayer.visible = true;
							vPlayer.play();
							stage.removeChild(_mc1);
						};
						
					}
					else
						{/* no poster para*/}
					//hin's edit
					
					this.addChild(vPlayer);
					
				}else{
				}
		
			}else{
				vPlayer.visible = false;
			}
		//}catch(e){ vPlayer.visible = false;}

		}
		
	}
	
}
