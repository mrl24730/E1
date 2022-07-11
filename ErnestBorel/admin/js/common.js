var now;

$("document").ready(function(){
	now = moment(system_time, "YYYY-MM-DD HH:mm:ss");
    updateTime();
    setInterval(updateTime, 1000);
});

function updateTime(){
    now.add(1, "second");
    $(".system_time").text(now.format("YYYY-MM-DD HH:mm:ss"));
}