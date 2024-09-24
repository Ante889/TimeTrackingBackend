import React, { useState } from 'react';
import { Layout, Button, Checkbox, Form, Input, theme, Typography, Row, Col, message, Spin } from 'antd';
import { useNavigate } from 'react-router-dom';
import { loginUser } from '../service/authService';

const { Content } = Layout;
const { Title } = Typography;

const Login = () => {
  const navigate = useNavigate();
  const {
    token: { colorBgContainer },
  } = theme.useToken();

  const [loading, setLoading] = useState(false);

  const onFinish = async (values) => {
    setLoading(true);
    try {
      await loginUser(values.email, values.password, navigate);
      message.success('Login successful');
    } catch (error) {
      message.error('Login failed, please try again');
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
            <Title level={2}>Login</Title>
          </div>
          <Form
            name="basic"
            labelCol={{ span: 24 }}
            wrapperCol={{ span: 24 }}
            style={{ maxWidth: 400, margin: 'auto' }}
            initialValues={{ remember: true }}
            onFinish={onFinish}
            onFinishFailed={onFinishFailed}
            autoComplete="off"
          >
            <Form.Item
              label="Email"
              name="email"
              rules={[{ required: true, message: 'Please input your email!' }]}
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
              name="remember"
              valuePropName="checked"
              wrapperCol={{ offset: 0, span: 24 }}
            >
              <Checkbox>Remember me</Checkbox>
            </Form.Item>

            <Form.Item wrapperCol={{ offset: 0, span: 24 }}>
              <Button type="primary" htmlType="submit" block disabled={loading}>
                {loading ? <Spin /> : 'Submit'}
              </Button>
            </Form.Item>
          </Form>
        </Col>
      </Row>
    </Content>
  );
};

export default Login;
