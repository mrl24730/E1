//Defines the top level Class
function Class() { }
Class.prototype.construct = function() {};
Class.extend = function(def) {
  var classDef = function() {
      if (arguments[0] !== Class) { this.construct.apply(this, arguments); }
  };
 
  var proto = new this(Class);
  var superClass = this.prototype;
 
  for (var n in def) {
      var item = def[n];                      
      if (item instanceof Function) item.$ = superClass;
      proto[n] = item;
  }
 
  classDef.prototype = proto;
 
  //Give this new class the same static extend method    
  classDef.extend = this.extend;      
  return classDef;
};



var instagramClass = Class.extend({
  gallery   : null,
  masonry   : null,
  loading   : false,
  apiDataCount: 5,
  paging    : 0,
  theEnd    : false,

  api   : {
    "hashTag" : "/api/getIGList.ashx",
    "approval": "api/setIGDisplay.ashx"
  },

  
  construct   : function(){},

  initUI : function(){
    var that = this;
    this.gallery = arguments[0];

    //$('#gallery-wrap .item, #gallery-wrap').css('transition-duration', '0s');


    /*this.gallery.masonry({
      columnWidth: 204,
      gutter: 22,
      itemSelector: '.item'
    });*/

     //Desktop version
      $(".loadingWrapper").imagesLoaded();
      //Infinity scroll for gallery page
      $(window).bind('mousewheel wheel', function(e){
        $(window).scroll();
      });
      //IE compatibility
      var mousewheelevt=(/Firefox/i.test(navigator.userAgent))? "DOMMouseScroll" : "mousewheel" //FF doesn't recognize mousewheel as of FF3.x
      if(document.attachEvent){ //if IE (and Opera depending on user setting)
        document.attachEvent("on"+mousewheelevt, function(){ $(window).scroll(); });
      }


      $(window).scroll(function(){
        if  ($(window).scrollTop() >= $(document).height() - $(window).height() - 20){
          that.getMore();
        }
      });
      that.getHashTag();

      //Parameter
      if($.urlParam("idx_photo")){
        that.getHashTag($.urlParam("idx_photo"));

      }


      /*$(document).on("click",".item",function(){

        
      });
        */
      $(document).on("click",".item",function(){
        that.setApproval($(this));
      });

        
      //$.magnificPopup.close();

  },

  resetProperty : function(){
    var that = this;
    var items = $("#gallery-wrap").find(".item");
    //$("#gallery-wrap").masonry( 'remove', items );
    //$("#gallery-wrap").masonry();
    that.paging = 1;
    that.theEnd = false;
  },

  getMore: function(){
    var that = this;
    that.getHashTag();
  },


  getHashTag: function(idx_photo){
    var that = this;
    if(!idx_photo && (that.loading || that.theEnd)) return;

    that.setLoading(true);

    
    
    var getObj = {r:new Date().getTime()};

    if(idx_photo){
      getObj.idx_photo = idx_photo;
    }else{
      getObj.paging = that.paging;
    }

    var url = that.api.hashTag;
    if(adminMode) url += "?adminMode=" + adminMode;

    $.ajax({
        url:url,
        type:"POST",
        dataType: "json",
        cache: false,
        data: getObj,
        success: function(response){

          if(idx_photo){
            var item = that.venderGrid(response[0]);
            $("#popup").empty();
            $("#popup").append(item);

            $.magnificPopup.open({
            items:{src: '#popup'},
            type: 'inline'
          });

          }else{
            that.venderContent({data:response});
            that.paging++;
          }
          

          

          if(response.length == 0) that.theEnd = true;
        }

    })
    

    /*var response = {"data":[{"idx_photo":"831220999914078364_13438663","username":"will_jtr","photo_low":"http://scontent-b.cdninstagram.com/hphotos-xaf1/t51.2885-15/10731724_781569618555474_1100248986_a.jpg","photo_std":"http://scontent-b.cdninstagram.com/hphotos-xaf1/t51.2885-15/10731724_781569618555474_1100248986_n.jpg","photo_thumb":"http://scontent-b.cdninstagram.com/hphotos-xaf1/t51.2885-15/10731724_781569618555474_1100248986_s.jpg"},{"idx_photo":"831102670713173804_336188884","username":"cbmbc","photo_low":"http://scontent-a.cdninstagram.com/hphotos-xpa1/t51.2885-15/927473_905773559434218_1448983386_a.jpg","photo_std":"http://scontent-a.cdninstagram.com/hphotos-xpa1/t51.2885-15/927473_905773559434218_1448983386_n.jpg","photo_thumb":"http://scontent-a.cdninstagram.com/hphotos-xpa1/t51.2885-15/927473_905773559434218_1448983386_s.jpg"},{"idx_photo":"831085428120784567_412338","username":"mrquentin","photo_low":"http://scontent-b.cdninstagram.com/hphotos-xaf1/t51.2885-15/10732023_684985781598246_1667530945_a.jpg","photo_std":"http://scontent-b.cdninstagram.com/hphotos-xaf1/t51.2885-15/10732023_684985781598246_1667530945_n.jpg","photo_thumb":"http://scontent-b.cdninstagram.com/hphotos-xaf1/t51.2885-15/10732023_684985781598246_1667530945_s.jpg"},{"idx_photo":"831074589049392615_412338","username":"mrquentin","photo_low":"http://scontent-b.cdninstagram.com/hphotos-xaf1/t51.2885-15/10723869_271896236267535_1535133013_a.jpg","photo_std":"http://scontent-b.cdninstagram.com/hphotos-xaf1/t51.2885-15/10723869_271896236267535_1535133013_n.jpg","photo_thumb":"http://scontent-b.cdninstagram.com/hphotos-xaf1/t51.2885-15/10723869_271896236267535_1535133013_s.jpg"},{"idx_photo":"831027146966229044_34082103","username":"chichungchung","photo_low":"http://scontent-a.cdninstagram.com/hphotos-xpa1/t51.2885-15/926255_1490993061165137_304152707_a.jpg","photo_std":"http://scontent-a.cdninstagram.com/hphotos-xpa1/t51.2885-15/926255_1490993061165137_304152707_n.jpg","photo_thumb":"http://scontent-a.cdninstagram.com/hphotos-xpa1/t51.2885-15/926255_1490993061165137_304152707_s.jpg"},{"idx_photo":"830815284745008727_6378132","username":"cwingpro","photo_low":"http://scontent-b.cdninstagram.com/hphotos-xaf1/t51.2885-15/10724124_364245410400028_1895772184_a.jpg","photo_std":"http://scontent-b.cdninstagram.com/hphotos-xaf1/t51.2885-15/10724124_364245410400028_1895772184_n.jpg","photo_thumb":"http://scontent-b.cdninstagram.com/hphotos-xaf1/t51.2885-15/10724124_364245410400028_1895772184_s.jpg"},{"idx_photo":"830780432962519950_222551247","username":"kelvin_yuen_","photo_low":"http://scontent-a.cdninstagram.com/hphotos-xaf1/t51.2885-15/10666143_803202093076455_1257377264_a.jpg","photo_std":"http://scontent-a.cdninstagram.com/hphotos-xaf1/t51.2885-15/10666143_803202093076455_1257377264_n.jpg","photo_thumb":"http://scontent-a.cdninstagram.com/hphotos-xaf1/t51.2885-15/10666143_803202093076455_1257377264_s.jpg"},{"idx_photo":"830476139732464886_6378132","username":"cwingpro","photo_low":"http://scontent-a.cdninstagram.com/hphotos-xap1/t51.2885-15/10518132_797737250270050_1399218505_a.jpg","photo_std":"http://scontent-a.cdninstagram.com/hphotos-xap1/t51.2885-15/10518132_797737250270050_1399218505_n.jpg","photo_thumb":"http://scontent-a.cdninstagram.com/hphotos-xap1/t51.2885-15/10518132_797737250270050_1399218505_s.jpg"},{"idx_photo":"830426762541135293_13964","username":"chrulases","photo_low":"http://scontent-a.cdninstagram.com/hphotos-xaf1/t51.2885-15/10727526_735053123233544_1372955917_a.jpg","photo_std":"http://scontent-a.cdninstagram.com/hphotos-xaf1/t51.2885-15/10727526_735053123233544_1372955917_n.jpg","photo_thumb":"http://scontent-a.cdninstagram.com/hphotos-xaf1/t51.2885-15/10727526_735053123233544_1372955917_s.jpg"},{"idx_photo":"830289567853565900_37012036","username":"felixhei","photo_low":"http://scontent-b.cdninstagram.com/hphotos-xaf1/t51.2885-15/10729467_624074671047075_1751469324_a.jpg","photo_std":"http://scontent-b.cdninstagram.com/hphotos-xaf1/t51.2885-15/10729467_624074671047075_1751469324_n.jpg","photo_thumb":"http://scontent-b.cdninstagram.com/hphotos-xaf1/t51.2885-15/10729467_624074671047075_1751469324_s.jpg"}]};
    that.venderContent(response);*/
  },

  venderContent : function(obj){
    var that = this;
    // console.log(obj);

    var div = $("<div/>");

    for(var i = 0; i < obj.data.length; i++){
      var post = obj.data[i];
      
      var item = that.venderGrid(post);
      //approval
      
      
      

      div.append(item);
    }

    var items = div.find(".item");
    $("#gallery-wrap").append(items);

     $("#gallery-wrap").imagesLoaded( function() {       
       console.log("reload");
       that.setLoading(false);
    }).progress(function(instance,image){
      if(image.isLoaded === false){
        items = items.not($(image.img).parent());
        $(image.img).parent().remove();
      }
     });

  },

  venderGrid :function(post){
    var that = this;
      var item = $("<div/>");
      var img = $("<img/>");
      item.addClass("item");
      item.attr("data-idx-photo",post.idx_photo);
      /*if(that.getGridSize()){
        item.addClass("item-big");
      }else{
        item.addClass("item-small");
      }*/
      img.attr("src",post.photo_low);
      item.attr("data-href",post.photo_std);
      var desc = $("<div/>");
      desc.addClass("desc");

      /*var username = $("<span/>");
      username.addClass("username").text("@" + post.username);
      desc.append(username);*/
      //desc.append(that.setLike(post.likes.count));
      item.append(desc);
      item.append(img);

      that.setApprovalDisplay(item,post.is_display);

      return item;
  },

  setApproval : function(item){
    var is_display = item.find(".rejected").length == 0;

    this.setApprovalAPI(item.attr("data-idx-photo"),!is_display);

    this.setApprovalDisplay(item,!is_display);


    //
    if(item.parents("#popup").length > 0){
        var altItem = $("#gallery-wrap").find(".item[data-idx-photo='{0}']".replace("{0}",item.attr("data-idx-photo")));

        if(altItem.length > 0){
          this.setApprovalDisplay(altItem,!is_display);
        }
             
        
    }
  },

  setApprovalAPI : function(idx_photo,is_display){

    var that = this;
    var getObj = {idx_photo:idx_photo,is_display:is_display};

    $.ajax({
      url:that.api.approval,
      type:"GET",
      dataType: "json",
      cache: false,
      data: getObj,
      success: function(response){
        
      }
    });

  },

  setApprovalDisplay : function(item,flag){
    var remark = item.find(".remark");
    var ico = remark.find(".ico");
    if(remark.length < 1){
      remark = $("<div/>");
      remark.addClass("remark");
      ico = $("<span/>");
      ico.addClass("ico");
      remark.append(ico);
      item.append(remark);
    }

    ico.removeClass("rejected");
    if(flag){
        ico.html("&#10004");
    }else{
      ico.html("&#10008");
      ico.addClass("rejected");
    }

  },

  setLoading : function(flag){
    var that = this;
    that.loading = flag;

    if(flag){
      $(".loadingSpin").show();
    }else{
      $(".loadingSpin").hide();
    }
  },

  getGridSize : function(){
    var that = this;
    var isBig =(Math.floor(Math.random() * 10) > 7);

    return isBig;
  },

  shuffle : function (o){ //v1.0
    for(var j, x, i = o.length; i; j = Math.floor(Math.random() * i), x = o[--i], o[i] = o[j], o[j] = x);
        return o;
    }
});


$.urlParam = function(name) {
  var results = new RegExp('[?&]' + name + '=([^&#]*)').exec(window.location.href);
  if (!results) {
    return 0;
  }
  return results[1] || 0;
}; // end urlParam setup