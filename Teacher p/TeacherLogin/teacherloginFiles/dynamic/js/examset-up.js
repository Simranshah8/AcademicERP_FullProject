
on_load =()=>{   
    document.getElementById('exam-setup-form').style.display="none";   
	
}
	window.onload=on_load();




    document.getElementById('add-exam-setup').onclick = function(){
	document.getElementById('table-listing').style.display="none";
	document.getElementById('exam-setup-form').style.display="block";
}

    document.getElementById('back-btn').onclick = function(){
	document.getElementById('table-listing').style.display="block";
	document.getElementById('exam-setup-form').style.display="none";
}
