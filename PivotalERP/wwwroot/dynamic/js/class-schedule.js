

  on_load =()=>{
	document.getElementById('class-shift-form').style.display="none";
    document.getElementById('period-management-form').style.display="none";
	document.getElementById('class-teacher-form').style.display="none";
	document.getElementById('hod-form').style.display="none";
	
}
	window.onload=on_load();

 
document.getElementById('add-class-shift').onclick = function(){
	document.getElementById('class-shift-content').style.display ="none";
    document.getElementById('class-shift-form').style.display = "block";
}

document.getElementById('back-btn-class-shift').onclick = function(){
	document.getElementById('class-shift-content').style.display="block";
	document.getElementById('class-shift-form').style.display="none";
}


// period Management



document.getElementById('add-period').onclick = function(){
	document.getElementById('period-management-content').style.display ="none";
    document.getElementById('period-management-form').style.display = "block";
}

document.getElementById('back-btn-period').onclick = function(){
	document.getElementById('period-management-content').style.display="block";
	document.getElementById('period-management-form').style.display="none";
}


// // class teacher

document.getElementById('add-class-teacher').onclick = function(){
	document.getElementById('class-teacher-content').style.display ="none";
    document.getElementById('class-teacher-form').style.display = "block";
}

document.getElementById('back-btn-class-teacher').onclick = function(){
	document.getElementById('class-teacher-content').style.display="block";
	document.getElementById('class-teacher-form').style.display="none";
}

// // HOD

document.getElementById('add-hod').onclick = function(){
	document.getElementById('hod-content').style.display ="none";
    document.getElementById('hod-form').style.display = "block";
}

document.getElementById('back-btn-hod').onclick = function(){
	document.getElementById('hod-content').style.display="block";
	document.getElementById('hod-form').style.display="none";
}