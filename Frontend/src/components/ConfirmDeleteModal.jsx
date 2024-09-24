import React from 'react';
import { Modal } from 'antd';

const ConfirmDeleteModal = ({ visible, onCancel, onConfirm, message }) => {
  return (
    <Modal
      title="Confirm Deletion"
      open={visible}
      onCancel={onCancel}
      onOk={onConfirm}
      okButtonProps={{ danger: true }}
    >
      <p>{message}</p>
    </Modal>
  );
};

export default ConfirmDeleteModal;
