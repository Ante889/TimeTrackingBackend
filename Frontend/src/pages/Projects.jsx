import React, { useState, useEffect } from 'react';
import { Layout, Button, Modal, Form, Input, Card, Col, Row, message, Spin } from 'antd';
import { PlusOutlined } from '@ant-design/icons';
import { getProjectsByUserId, createProject, updateProject, deleteProject } from '../service/projectService';
import ConfirmDeleteModal from '../components/ConfirmDeleteModal';
import Breadcrumbs from '../components/Breadcrumbs';

const { Content } = Layout;

const Projects = () => {
  const [projects, setProjects] = useState([]);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [isDeleteModalOpen, setIsDeleteModalOpen] = useState(false);
  const [editingProject, setEditingProject] = useState(null);
  const [loading, setLoading] = useState(false);
  const [projectToDelete, setProjectToDelete] = useState(null);
  const [form] = Form.useForm();

  const userId = localStorage.getItem('userId');

  useEffect(() => {
    fetchProjects();
  }, []);

  const fetchProjects = async () => {
    setLoading(true);
    try {
      const data = await getProjectsByUserId(userId);
      setProjects(Array.isArray(data) ? data : []);
    } catch (error) {
      message.error('Failed to load projects.');
    }
    setLoading(false);
  };

  const handleOpenModal = (project = null) => {
    setEditingProject(project);
    setIsModalOpen(true);
    form.setFieldsValue(project || { name: '', description: '' });
  };

  const handleCancel = () => {
    setIsModalOpen(false);
    form.resetFields();
  };

  const handleFinish = async (values) => {
    setLoading(true);
    try {
      if (editingProject) {
        await updateProject(editingProject.id, values);
        message.success('Project updated successfully!');
      } else {
        await createProject({ ...values, userCreated: userId });
        message.success('Project created successfully!');
      }
      fetchProjects();
      handleCancel();
    } catch (error) {
      message.error('Failed to save project.');
    }
    setLoading(false);
  };

  const handleDeleteProject = async () => {
    setLoading(true);
    try {
      await deleteProject(projectToDelete.id);
      message.success('Project deleted successfully!');
      fetchProjects();
      setIsDeleteModalOpen(false);
    } catch (error) {
      message.error('Failed to delete project.');
    }
    setLoading(false);
  };

  return (
    <Content style={{ margin: '0 16px' }}>
      <Breadcrumbs items={[{ label: 'Projects', route: '/projects' }]} />
      <div style={{ padding: 24, minHeight: 360, background: '#fff' }}>
        <Button type="primary" icon={<PlusOutlined />} onClick={() => handleOpenModal()}>
          Add Project
        </Button>
        {loading ? (
          <div style={{ textAlign: 'center', marginTop: 50 }}>
            <Spin size="large" />
          </div>
        ) : (
          <Row gutter={[16, 16]} style={{ marginTop: 16 }}>
            {Array.isArray(projects) && projects.length > 0 ? (
              projects.map((project) => (
                <Col key={project.id} xs={24} sm={12} md={8}>
                  <Card
                    title={project.name}
                    bordered={false}
                    style={{ boxShadow: '0 4px 8px rgba(0, 0, 0, 0.2)' }}
                  >
                    <p>{project.description.length > 150 ? `${project.description.slice(0, 150)}...` : project.description}</p>
                    <Button type="default" style={{ width: '100%', marginBottom: '10px' }} onClick={() => {/* Logika za prikaz faza */}}>
                      Choice project
                    </Button>
                    <div style={{ display: 'flex', justifyContent: 'space-between' }}>
                      <Button type="primary" style={{ flex: 1, marginRight: '8px' }} onClick={() => handleOpenModal(project)}>Edit</Button>
                      <Button danger type="primary" style={{ flex: 1 }} onClick={() => { setProjectToDelete(project); setIsDeleteModalOpen(true); }}>Delete</Button>
                    </div>
                  </Card>
                </Col>
              ))
            ) : (
              <div style={{ textAlign: 'center', width: '100%' }}>No projects available.</div>
            )}
          </Row>
        )}
      </div>

      <Modal
        title={editingProject ? 'Edit Project' : 'Add Project'}
        open={isModalOpen}
        onCancel={handleCancel}
        onOk={() => form.submit()}
      >
        <Form form={form} onFinish={handleFinish} layout="vertical">
          <Form.Item name="name" label="Project Name" rules={[{ required: true, message: 'Please enter project name' }]}>
            <Input />
          </Form.Item>
          <Form.Item name="description" label="Description" rules={[{ required: true, message: 'Please enter project description' }]}>
            <Input.TextArea />
          </Form.Item>
        </Form>
      </Modal>

      <ConfirmDeleteModal
        visible={isDeleteModalOpen}
        onCancel={() => setIsDeleteModalOpen(false)}
        onConfirm={handleDeleteProject}
        message="Are you sure you want to delete this project?"
      />
    </Content>
  );
};

export default Projects;
