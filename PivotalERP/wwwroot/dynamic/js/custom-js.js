on_load =()=>{
	
	document.getElementById('enquiry-form').style.display="none";
    document.getElementById('no-option').style.display="none";
	
}
	window.onload=on_load();

// Radio Button Click
document.getElementById('open-form-btn').onclick = function(){
	document.getElementById('table-listing').style.display="none";
	document.getElementById('enquiry-form').style.display="block";
   
}
document.getElementById('back-to-list').onclick = function(){
	document.getElementById('table-listing').style.display="block";
    
	document.getElementById('enquiry-form').style.display="none";
}

// Yes/No radio button
document.getElementById('teacher-no').onclick = function(){
	document.getElementById('no-option').style.display="block";    
	document.getElementById('teacher-subject').style.display="none";
	document.getElementById('lavel').style.display="none";
}


document.getElementById('teacher-yes').onclick = function(){
	document.getElementById('no-option').style.display="none";    
	document.getElementById('teacher-subject').style.display="block";
	document.getElementById('lavel').style.display="block";
}