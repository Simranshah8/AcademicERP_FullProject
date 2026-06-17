
on_load =()=>{	
	
	document.getElementById('add-homework-form').style.display="none";
	document.getElementById('detail-form').style.display="none";		
}
window.onload=on_load();



// Add Homework
document.getElementById('add-homework-btn').onclick = function(){	
document.getElementById('add-homework-section').style.display="none";
document.getElementById('add-homework-form').style.display="block";	   
}
	
document.getElementById('back-add-homework-btn').onclick = function(){
document.getElementById('add-homework-form').style.display="none";		
document.getElementById('add-homework-section').style.display="block";
}

// / Homework Listadd-hom
document.getElementById('show-details-btn').onclick = function(){	
document.getElementById('homework-list-section').style.display="none";
document.getElementById('detail-form').style.display="block";	   
}
	
document.getElementById('back-homework-list-btn').onclick = function(){
document.getElementById('detail-form').style.display="none";		
document.getElementById('homework-list-section').style.display="block";
}



	