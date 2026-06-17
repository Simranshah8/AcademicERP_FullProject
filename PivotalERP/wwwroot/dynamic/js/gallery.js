on_load =()=>{	
	document.getElementById('notice-form').style.display="none";		
}
	window.onload=on_load();

// Radio Button Click
document.getElementById('open-form-btn').onclick = function(){
	document.getElementById('table-listing').style.display="none";
	document.getElementById('notice-form').style.display="block";   
}
document.getElementById('back-to-list').onclick = function(){
	document.getElementById('table-listing').style.display="block";    
	document.getElementById('notice-form').style.display="none";
}
