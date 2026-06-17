// Visitor Enquiry
on_load =()=>{
	document.getElementById('add-subject-form').style.display="none";
	
}
	window.onload=on_load();

//Phonecall Log section
document.getElementById('add-subject').onclick = function(){
	document.getElementById('add-subject-section').style.display="none";
	document.getElementById('add-subject-form').style.display="block";
}
document.getElementById('back-to-subject-list-btn').onclick = function(){
	document.getElementById('add-subject-form').style.display="none";
	document.getElementById('add-subject-section').style.display="block";
}
// document.getElementById('edit-calllog-enquiry').onclick = function(){
// 	document.getElementById('add-subject-section').style.display="none";
// 	document.getElementById('add-subject-form').style.display="block";
// }
