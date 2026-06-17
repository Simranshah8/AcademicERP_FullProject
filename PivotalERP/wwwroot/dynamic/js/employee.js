
  on_load =()=>{
    document.getElementById('department-form').style.display="none";
    document.getElementById('designation-form').style.display="none";
    document.getElementById('level-form').style.display="none";
    document.getElementById('service-type-form').style.display="none";
    document.getElementById('category-form').style.display="none";
    
	
}
	window.onload=on_load();

// Department section

document.getElementById('add-department').onclick=function(){
    document.getElementById('department-section').style.display="none";
    document.getElementById('department-form').style.display="block";
}

document.getElementById('back-department-list').onclick = function(){
  document.getElementById('department-section').style.display="block";
  document.getElementById('department-form').style.display="none";
}



// Designation section

document.getElementById('add-designation').onclick=function(){
    document.getElementById('designation-section').style.display="none";
    document.getElementById('designation-form').style.display="block";
}

document.getElementById('back-designation-list').onclick = function(){
  document.getElementById('designation-section').style.display="block";
  document.getElementById('designation-form').style.display="none";
}

// Level section

document.getElementById('add-level').onclick=function(){
    document.getElementById('level-section').style.display="none";
    document.getElementById('level-form').style.display="block";
}

document.getElementById('back-level-list').onclick = function(){
  document.getElementById('level-section').style.display="block";
  document.getElementById('level-form').style.display="none";
}

// service type
document.getElementById('add-service-type').onclick=function(){
    document.getElementById('service-type-section').style.display="none";
    document.getElementById('service-type-form').style.display="block";
}

document.getElementById('back-service-type-list').onclick = function(){
  document.getElementById('service-type-section').style.display="block";
  document.getElementById('service-type-form').style.display="none";
}

// category
document.getElementById('add-category').onclick=function(){
  document.getElementById('category-section').style.display="none";
  document.getElementById('category-form').style.display="block";
}

document.getElementById('back-category-list').onclick = function(){
document.getElementById('category-section').style.display="block";
document.getElementById('category-form').style.display="none";
}
