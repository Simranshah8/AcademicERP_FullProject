on_load =()=>{
	
	document.getElementById('seat-allotment-form').style.display="none";
   
	
}
	window.onload=on_load();

// Radio Button Click
document.getElementById('add-seat-allotment-btn').onclick = function(){
	document.getElementById('seat-allotment-listing').style.display="none";
	document.getElementById('seat-allotment-form').style.display="block";
   
}
document.getElementById('back-allotment-list').onclick = function(){
	document.getElementById('seat-allotment-listing').style.display="block";
    
	document.getElementById('seat-allotment-form').style.display="none";
}
