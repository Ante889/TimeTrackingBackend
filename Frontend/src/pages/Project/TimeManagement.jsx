import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { getTimesByCategoryId, createTime, updateTime, deleteTime } from '../../service/timeService';
import { Button, Modal, Form, Input, message, Collapse } from 'antd';
import { PlusOutlined } from '@ant-design/icons';

const { Panel } = Collapse;

const TimeManagement = ({ categoryId }) => {
  const navigate = useNavigate();
  const [times, setTimes] = useState([]);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [form] = Form.useForm();
  const [editingTime, setEditingTime] = useState(null);
  const [timeToDelete, setTimeToDelete] = useState(null);

  useEffect(() => {
    fetchTimes();
  }, [categoryId]);

  const fetchTimes = async () => {
    try {
      const data = await getTimesByCategoryId(categoryId, navigate);
      setTimes(data);
    } catch (error) {
      console.error('Failed to fetch times:', error);
    }
  };

  const handleAddTime = () => {
    setEditingTime(null);
    form.resetFields();
    setIsModalOpen(true);
  };

  const handleEditTime = (time) => {
    setEditingTime(time);
    form.setFieldsValue({ title: time.title, description: time.description, timeInMinutes: time.timeInMinutes });
    setIsModalOpen(true);
  };

  const handleDeleteTime = (time) => {
    setTimeToDelete(time);
    setIsModalOpen(true);
  };

  const handleConfirmDeleteTime = async () => {
    try {
      await deleteTime(timeToDelete.id, navigate);
      message.success('Time deleted successfully!');
      setTimes((prevTimes) => prevTimes.filter((t) => t.id !== timeToDelete.id));
    } catch (error) {
      message.error('Failed to delete time.');
    }
  };

  const handleFinishTime = async (values) => {
    try {
      if (editingTime) {
        await updateTime(editingTime.id, values, navigate);
        message.success('Time updated successfully!');
      } else {
        await createTime({ category: categoryId, ...values }, navigate);
        message.success('Time added successfully!');
      }
      form.resetFields();
      setIsModalOpen(false);
      fetchTimes();
    } catch (error) {
      message.error('Failed to save time.');
    }
  };

  return (
    <>
      <Button type="primary"  icon={<PlusOutlined />} onClick={handleAddTime}>
        Add Time
      </Button>
      <Collapse>
        <Panel header="Time" key="1">
          {times.length > 0 ? (
            times.map((time) => (
              <div key={time.id} style={{ marginTop: '10px' }}>
                <b>{time.title} - {time.timeInMinutes} min</b>
                <Button type="primary" onClick={() => handleEditTime(time)} style={{ marginLeft: '8px', marginRight:'8px' }}>
                  Edit
                </Button>
                <Button type="primary" danger onClick={() => handleDeleteTime(time)}>
                  Delete
                </Button>
                <br />
                <span>{time.description}</span>
                <hr />
              </div>
            ))
          ) : (
            <p>No times available for this category.</p>
          )}
        </Panel>
      </Collapse>

      <Modal
        title={editingTime ? 'Edit Time' : 'Add Time'}
        open={isModalOpen}
        onCancel={() => setIsModalOpen(false)}
        onOk={() => form.submit()}
      >
        <Form form={form} onFinish={handleFinishTime} layout="vertical">
          <Form.Item
            name="title"
            label="Title"
            rules={[{ required: true, message: 'Please enter title' }]}
          >
            <Input />
          </Form.Item>
          <Form.Item
            name="description"
            label="Description"
          >
            <Input.TextArea />
          </Form.Item>
          <Form.Item
            name="timeInMinutes"
            label="Time in Minutes"
            rules={[{ required: true, message: 'Please enter time in minutes' }]}
          >
            <Input type="number" />
          </Form.Item>
        </Form>
      </Modal>

      <Modal
        title="Confirm Delete"
        open={!!timeToDelete}
        onCancel={() => setTimeToDelete(null)}
        onOk={handleConfirmDeleteTime}
      >
        <p>Are you sure you want to delete this time?</p>
      </Modal>
    </>
  );
};

export default TimeManagement;
