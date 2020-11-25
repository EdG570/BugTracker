var ui = (function () {
    var $form = $('#project-edit-form');
    var $cardCont = $('#project-card-cont');

    var populateForm = function (project) {
        $form.find('#project-edit-name').val(project.name);
        $form.find('#project-edit-description').val(project.description);
        $form.find('#project-edit-link').val(project.repositoryUri);
    };

    var updateCard = function (project) {
        var id = '#' + project.id.toString();
        $cardCont.find(id).find('.card-title').text(project.name);
        modals.toggleEditModal();
    };

    return {
        populateForm: populateForm,
        updateCard: updateCard
    };

})();

var ajax = (function () {

    var getProjectById = function (id) {
        $.ajax({
            type: "POST",
            url: '/Project/FindById',
            data: { id: id },
            success: function (data) {
                ui.populateForm(data.project);
                modals.toggleEditModal();

                return data.project;
            },
            error: function (e) {

            }
        });
    };

    var updateProject = function (name, description, link, projectId) {
        $.ajax({
            type: "POST",
            url: '/Project/Edit',
            data: { name, description, link, projectId },
            success: function (data) {
                ui.updateCard(data.project);
            },
            error: function (e) {

            }
        });
    };

    return {
        getProjectById: getProjectById,
        updateProject: updateProject
    };

})();

var edit = (function () {
    var $form = $('#project-edit-form');
    var $formBtn = $('#edit-form-btn');
    var $editBtn = $('.project-edit-btn');
    var currentProjectId = 0;

    $editBtn.on('click', function () {
        currentProjectId = $(this).prev('button').prev('.project-detail-link').attr('id');
        ajax.getProjectById(currentProjectId);
    });

    $formBtn.on('click', function (e) {
        e.preventDefault();

        var name = $form.find('#project-edit-name').val();
        var description = $form.find('#project-edit-description').val();
        var link = $form.find('#project-edit-link').val();

        ajax.updateProject(name, description, link, currentProjectId);
    });

    return {

    };

})();

var modals = (function () {
    var projectCreateModal = $('#project-create-modal');
    var projectEditModal = $('#project-edit-modal');
    var projectCreateBtn = $('#project-create-btn');
    var projectEditBtn = $('.project-edit-btn');

    projectCreateBtn.on('click', function () {
        projectCreateModal.modal('toggle');
    });

    var toggleEditModal = function () {
        projectEditModal.modal('toggle');
    };

    var init = function () {
        
    };

    return {
        init: init,
        toggleEditModal: toggleEditModal
    };
})();

var projectDetails = (function () {
    var $detailsBtn = $('.project-details-btn');

    var updateUiModal = function (project) {
        var $modal = $('#project-detail-modal');

        $modal.find('#project-detail-name').text(project.name);
        $modal.find('.project-owner').text(project.createdBy);
        $modal.find('.project-description').text(project.description);
        $modal.find('.project-repo-uri').text(project.repositoryUri);
        $modal.find('.project-collaborators').text(project.userProjects.length);
        $modal.find('.project-tickets').text(project.tickets.length);
        $modal.find('.project-start-date').text(project.createdAt);

        $modal.modal('toggle');
        console.log(project);
    };

    var getProjectById = function (id) {
        $.ajax({
            type: "POST",
            url: '/Project/FindById',
            data: { id: id },
            success: function (data) {
                updateUiModal(data.project);
            },
            error: function (e) {

            }
        });
    };

    $detailsBtn.on('click', function () {
        var projectId = $(this).prev('a').attr('id');
        getProjectById(projectId);
    });

    return {

    };
})();

$(document).ready(function () {

});