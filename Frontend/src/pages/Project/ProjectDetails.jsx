import React, { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { getPhasesByProjectId, createPhase, updatePhase, deletePhase } from '../../service/phaseService';
import { Card, Layout, Row, Col, Spin, Badge, Button, Modal, Form, Input, message, Collapse } from 'antd';
import ConfirmDeleteModal from '../../components/ConfirmDeleteModal';
import Breadcrumbs from '../../components/Breadcrumbs';
import { PlusOutlined } from '@ant-design/icons';

const { Content } = Layout;
const { Panel } = Collapse;

const ProjectDetail = () => {
  const { id: projectId } = useParams();
  const navigate = useNavigate();
  const [phases, setPhases] = useState([]);
  const [loading, setLoading] = useState(true);
  const [isDeleteModalOpen, setIsDeleteModalOpen] = useState(false);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [form] = Form.useForm();
  const [editingPhase, setEditingPhase] = useState(null);
  const [phaseToDelete, setPhaseToDelete] = useState(null);

  useEffect(() => {
    const fetchPhases = async () => {
      try {
        const data = await getPhasesByProjectId(projectId, navigate);
        setPhases(data);
      } catch (error) {
        console.error('Failed to fetch phases:', error);
      } finally {
        setLoading(false);
      }
    };

    fetchPhases();
  }, [projectId, navigate]);

  const handleAddPhase = () => {
    setEditingPhase(null);
    form.resetFields();
    setIsModalOpen(true);
  };

  const handleEditPhase = (phase) => {
    setEditingPhase(phase);
    form.setFieldsValue({ description: phase.description, amountPaid: phase.amountPaid });
    setIsModalOpen(true);
  };

  const handleDeletePhase = (phase) => {
    setPhaseToDelete(phase);
    setIsDeleteModalOpen(true);
  };

  const handleConfirmDelete = async () => {
    try {
      await deletePhase(phaseToDelete.id, navigate);
      message.success('Phase deleted successfully!');
      setIsDeleteModalOpen(false);
      setPhases((prevPhases) => prevPhases.filter((phase) => phase.id !== phaseToDelete.id));
    } catch (error) {
      message.error('Failed to delete phase.');
    }
  };

  const handleFinish = async (values) => {
    try {
      if (editingPhase) {
        await updatePhase(editingPhase.id, values, navigate);
        message.success('Phase updated successfully!');
      } else {
        await createPhase({ project: projectId, ...values }, navigate);
        message.success('Phase added successfully!');
      }
      form.resetFields();
      setIsModalOpen(false);
      const updatedPhases = await getPhasesByProjectId(projectId, navigate);
      setPhases(updatedPhases);
    } catch (error) {
      message.error('Failed to save phase.');
    }
  };

  return (
    <Layout style={{ margin: '0 16px' }}>
      <Breadcrumbs items={[{ label: 'Projects', route: '/projects' }, { label: 'Phase', route: '/project-detail/' + projectId }]} />
      <Content>
        <Button type="primary" icon={<PlusOutlined />} onClick={handleAddPhase} style={{ marginTop: '20px', marginBottom: '20px' }}>
          Add Phase
        </Button>
        <br />
        {loading ? (
          <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '50vh' }}>
            <Spin size="large" />
          </div>
        ) : (
          <Row gutter={[16, 16]}>
            {phases.length > 0 ? (
              phases.map((phase) => (
                <Col span={24} key={phase.id}>
                  <Card
                    bordered={false}
                    style={{ width: '100%', marginBottom: '16px' }}
                    title={
                      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}
                      onClick={() => navigate(`/project-detail/category/${projectId}/${phase.id}`)}
                      > 
                        <span style={{ cursor: 'pointer', color: '#1890ff', fontWeight: 'bold' }}>
                          {`Phase ${phase.phaseNumber}`}
                        </span>
                        {
                          phase.totalCost - phase.amountPaid > 0 ? (
                            <Badge count={`$${phase.totalCost - phase.amountPaid}`} style={{ backgroundColor: 'red' }} />
                          ) : (
                            <Badge count={`$${phase.totalCost - phase.amountPaid}`} style={{ backgroundColor: 'green' }} />
                          )
                        }
                      </div>
                    }
                  >
                    <Collapse>
                      <Panel header="More Info" key="1">
                        <p>{phase.description}</p>
                      </Panel>
                    </Collapse>
                    <div style={{ marginTop: '10px' }}>
                      <Button type="primary" onClick={() => handleEditPhase(phase)} style={{  marginRight: '8px' }}>
                        Edit
                      </Button>
                      <Button danger onClick={() => handleDeletePhase(phase)}>
                        Delete
                      </Button>
                    </div>
                  </Card>
                </Col>
              ))
            ) : (
              <p>No phases available for this project.</p>
            )}
          </Row>
        )}
      </Content>

      <Modal
        title={editingPhase ? 'Edit Phase' : 'Add Phase'}
        open={isModalOpen}
        onCancel={() => setIsModalOpen(false)}
        onOk={() => form.submit()}
      >
        <Form form={form} onFinish={handleFinish} layout="vertical">
          <Form.Item
            name="description"
            label="Description"
            rules={[{ required: true, message: 'Please enter phase description' }]}
          >
            <Input.TextArea />
          </Form.Item>
          <Form.Item
            name="amountPaid"
            label="Amount Paid"
            rules={[{ required: true, message: 'Please enter amount paid' }]}
          >
            <Input type="number" />
          </Form.Item>
        </Form>
      </Modal>

      <ConfirmDeleteModal
        visible={isDeleteModalOpen}
        onCancel={() => setIsDeleteModalOpen(false)}
        onConfirm={handleConfirmDelete}
        message="Are you sure you want to delete this phase?"
      />
    </Layout>
  );
};

export default ProjectDetail;
