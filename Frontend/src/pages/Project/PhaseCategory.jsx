import React, { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { getCategoriesByPhaseId, createCategory, updateCategory, deleteCategory } from '../../service/categoryService';
import { Card, Layout, Row, Col, Spin, Button, Modal, Form, Input, message, Collapse, Badge } from 'antd';
import ConfirmDeleteModal from '../../components/ConfirmDeleteModal';
import Breadcrumbs from '../../components/Breadcrumbs';
import { PlusOutlined } from '@ant-design/icons';
import TimeManagement from './TimeManagement'; 

const { Content } = Layout;

const PhaseCategory = () => {
  const { projectId, phaseId } = useParams();
  const navigate = useNavigate();
  const [categories, setCategories] = useState([]);
  const [loading, setLoading] = useState(true);
  const [isDeleteModalOpen, setIsDeleteModalOpen] = useState(false);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [form] = Form.useForm();
  const [editingCategory, setEditingCategory] = useState(null);
  const [categoryToDelete, setCategoryToDelete] = useState(null);

  useEffect(() => {
    const fetchCategories = async () => {
      try {
        const data = await getCategoriesByPhaseId(phaseId, navigate);
        setCategories(data);
      } catch (error) {
        console.error('Failed to fetch categories:', error);
      } finally {
        setLoading(false);
      }
    };

    fetchCategories();
  }, [phaseId, navigate]);

  const handleAddCategory = () => {
    setEditingCategory(null);
    form.resetFields();
    setIsModalOpen(true);
  };

  const handleEditCategory = (category) => {
    setEditingCategory(category);
    form.setFieldsValue({ name: category.name, pricePerHour: category.pricePerHour });
    setIsModalOpen(true);
  };

  const handleDeleteCategory = (category) => {
    setCategoryToDelete(category);
    setIsDeleteModalOpen(true);
  };

  const handleConfirmDeleteCategory = async () => {
    try {
      await deleteCategory(categoryToDelete.id, navigate);
      message.success('Category deleted successfully!');
      setIsDeleteModalOpen(false);
      setCategories((prevCategories) => prevCategories.filter((cat) => cat.id !== categoryToDelete.id));
    } catch (error) {
      message.error('Failed to delete category.');
    }
  };

  const handleFinishCategory = async (values) => {
    try {
      if (editingCategory) {
        await updateCategory(editingCategory.id, values, navigate);
        message.success('Category updated successfully!');
      } else {
        await createCategory({ phase: phaseId, ...values }, navigate);
        message.success('Category added successfully!');
      }
      form.resetFields();
      setIsModalOpen(false);
      const updatedCategories = await getCategoriesByPhaseId(phaseId, navigate);
      setCategories(updatedCategories);
    } catch (error) {
      message.error('Failed to save category.');
    }
  };

  return (
    <Layout style={{ margin: '0 16px' }}>
      <Breadcrumbs items={
        [
            { label: 'Projects', route: '/projects' }, 
            { label: 'Phase', route: '/project-detail/' + projectId },
            { label: 'Phase Categories', route: '/project-detail/category/' + projectId + '/' + phaseId }
        ]} />
      <Content>
        <Button type="primary" icon={<PlusOutlined />} onClick={handleAddCategory} style={{ marginTop: '20px', marginBottom: '20px' }}>
          Add Category
        </Button>
        {loading ? (
          <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '50vh' }}>
            <Spin size="large" />
          </div>
        ) : (
          <Row gutter={[16, 16]}>
            {categories.length > 0 ? (
              categories.map((category) => (
                <Col span={24} key={category.id}>
                  <Card bordered={false} style={{ width: '100%', marginBottom: '16px' }}>
                    <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
                      <span style={{ fontWeight: 'bold' }}>{category.name}</span>
                      <Badge count={`Price per hour: €${category.pricePerHour}`} style={{ backgroundColor: '#52c41a' }} />
                    </div>
                    <br />
                    <TimeManagement categoryId={category.id} />
                    <div style={{ marginTop: '10px' }}>
                      <Button type="primary" onClick={() => handleEditCategory(category)} style={{ marginRight: '8px' }}>
                        Edit
                      </Button>
                      <Button danger onClick={() => handleDeleteCategory(category)}>
                        Delete
                      </Button>
                    </div>
                  </Card>
                </Col>
              ))
            ) : (
              <p>No categories available for this phase.</p>
            )}
          </Row>
        )}
      </Content>

      <Modal
        title={editingCategory ? 'Edit Category' : 'Add Category'}
        open={isModalOpen}
        onCancel={() => setIsModalOpen(false)}
        onOk={() => form.submit()}
      >
        <Form form={form} onFinish={handleFinishCategory} layout="vertical">
          <Form.Item
            name="name"
            label="Name"
            rules={[{ required: true, message: 'Please enter category name' }]}
          >
            <Input />
          </Form.Item>
          <Form.Item
            name="pricePerHour"
            label="Price Per Hour"
            rules={[{ required: true, message: 'Please enter price per hour' }]}
          >
            <Input type="number" />
          </Form.Item>
        </Form>
      </Modal>

      <ConfirmDeleteModal
        visible={isDeleteModalOpen}
        onCancel={() => setIsDeleteModalOpen(false)}
        onConfirm={handleConfirmDeleteCategory}
        message="Are you sure you want to delete this category?"
      />
    </Layout>
  );
};

export default PhaseCategory;
