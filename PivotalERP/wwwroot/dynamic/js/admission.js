
// Admission Enquiry
on_load =()=>{
	document.getElementById('admission-enquiry-form').style.display="none";
	
}
	window.onload=on_load();

// sections display and hide
document.getElementById('add-admission-enquiry').onclick = function(){
	document.getElementById('admission-section').style.display="none";
	document.getElementById('admission-enquiry-form').style.display="block";
}
document.getElementById('admission-list-back-btn').onclick = function(){
	document.getElementById('admission-enquiry-form').style.display="none";
	document.getElementById('admission-section').style.display="block";
}