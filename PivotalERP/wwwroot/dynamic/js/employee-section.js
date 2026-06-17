// Visitor Enquiry
on_load =()=>{
	document.getElementById('left-employee-form').style.display="none";
	
}
	window.onload=on_load();

// sections display and hide
document.getElementById('add-employee-left').onclick = function(){
	document.getElementById('employee-left-section').style.display="none";
	document.getElementById('left-employee-form').style.display="block";
}
document.getElementById('employee-left-back-btn').onclick = function(){
	document.getElementById('left-employee-form').style.display="none";
	document.getElementById('employee-left-section').style.display="block";
}
