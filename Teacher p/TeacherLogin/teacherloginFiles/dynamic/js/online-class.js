
on_load =()=>{   
    document.getElementById('show-attendance-part').style.display="none";
    
	
}
	window.onload=on_load();




    document.getElementById('show-attendance').onclick = function(){
	document.getElementById('listing part').style.display="none";
	document.getElementById('show-attendance-part').style.display="block";
}

    document.getElementById('back-btn').onclick = function(){
	document.getElementById('listing part').style.display="block";
	document.getElementById('show-attendance-part').style.display="none";
}


