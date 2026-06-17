on_load =()=>{
    document.getElementById('classwise-form').style.display="none";
    document.getElementById('studentwise-form').style.display="none";
   
}
	window.onload=on_load();


document.getElementById('add-classwise').onclick = function(){
	document.getElementById('classwise-section').style.display="none";
	document.getElementById('classwise-form').style.display="block";
}
document.getElementById('classwiseback-btn').onclick = function(){
	document.getElementById('classwise-form').style.display="none";
	document.getElementById('classwise-section').style.display="block";
}



document.getElementById('add-studentwise').onclick = function(){
	document.getElementById('studentwise-section').style.display="none";
	document.getElementById('studentwise-form').style.display="block";
}
document.getElementById('studentwiseback-btn').onclick = function(){
	document.getElementById('studentwise-form').style.display="none";
	document.getElementById('studentwise-section').style.display="block";
}


