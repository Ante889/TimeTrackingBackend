import React, { useState } from 'react';
import { Layout, Button, Form, Input, theme, Typography, Row, Col, message, Spin } from 'antd';
import { registerUser } from '../service/authService';
import { useNavigate } from 'react-router-dom';

const { Content } = Layout;
const { Title } = Typography;

const Register = () => {
  const navigate = useNavigate();
  const {
    token: { colorBgContainer },
  } = theme.useToken();
  const [loading, setLoading] = useState(false);

  const onFinish = async (values) => {
    setLoading(true);
    try {
      await registerUser(
        values.email, 
        values.password,
        values.confirm,
        values.firstName,
        values.lastName,
        navigate
      );
      message.success('Registration successful');
    } catch (error) {
      message.error(error.response.data);
    } finally {
      setLoading(false);
    }
  };

  const onFinishFailed = (errorInfo) => {
    console.log('Failed:', errorInfo);
  };

  return (
    <Content style={{ background: colorBgContainer, padding: '50px 0' }}>
      <Row justify="center" align="middle" style={{ height: '100%' }}>
        <Col xs={24} sm={12} md={8}>
          <div style={{ textAlign: 'center', marginBottom: 24 }}>
            <Title level={2}>Register</Title>
          </div>
          <Form
            name="register"
            labelCol={{ span: 24 }}
            wrapperCol={{ span: 24 }}
            style={{ maxWidth: 400, margin: 'auto' }}
            initialValues={{ remember: true }}
            onFinish={onFinish}
            onFinishFailed={onFinishFailed}
            autoComplete="off"
          >
            <Form.Item
              label="First Name"
              name="firstName"
              rules={[{ required: true, message: 'Please input your first name!' }]}
            >
              <Input />
            </Form.Item>

            <Form.Item
              label="Last Name"
              name="lastName"
              rules={[{ required: true, message: 'Please input your last name!' }]}
            >
              <Input />
            </Form.Item>

            <Form.Item
              label="Email"
              name="email"
              rules={[{ type: 'email', required: true, message: 'Please input your email!' }]}
            >
              <Input />
            </Form.Item>

            <Form.Item
              label="Password"
              name="password"
              rules={[{ required: true, message: 'Please input your password!' }]}
            >
              <Input.Password />
            </Form.Item>

            <Form.Item
              label="Confirm Password"
              name="confirm"
              dependencies={['password']}
              hasFeedback
              rules={[
                { required: true, message: 'Please confirm your password!' },
                ({ getFieldValue }) => ({
                  validator(_, value) {
                    if (!value || getFieldValue('password') === value) {
                      return Promise.resolve();
                    }
                    return Promise.reject(new Error('The two passwords that you entered do not match!'));
                  },
                }),
              ]}
            >
              <Input.Password />
            </Form.Item>

            <Form.Item wrapperCol={{ offset: 0, span: 24 }}>
              <Button type="primary" htmlType="submit" block>
                {loading ? <Spin size="small" /> : 'Register'}
              </Button>
            </Form.Item>
          </Form>
        </Col>
      </Row>
    </Content>
  );
};

export default Register;
